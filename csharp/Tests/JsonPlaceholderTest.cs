using System.Net;
using TestTask2.Services;

namespace TestTask2.Tests
{
    public class JsonPlaceholderTest : IClassFixture<ServiceFixture>
    {
        private readonly PostsService _postsService;

        public JsonPlaceholderTest(ServiceFixture fixture)
        {
            _postsService = fixture.PostsService;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public async Task GetsPost_WithValidId_Returns200Ok(int expectedId)
        {
            // Act
            var response = await _postsService.GetPostByIdAsync(expectedId);
            var statusCode = response.StatusCode;
            var responseData = response.Data;

            // Assert
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.NotNull(response.Data);

            Assert.Multiple(
                () => Assert.True(responseData!.Id > 0, "Id must be grater than 0"),
                () => Assert.True(responseData!.UserId > 0, "UserId must be grater than 0"),
                () => Assert.False(string.IsNullOrEmpty(responseData!.Body), "Body should not be null or empty"),
                () => Assert.False(string.IsNullOrEmpty(responseData!.Title), "Title should not be null or empty")
                );
            Assert.Equal(expectedId, responseData!.Id);
        }

        [Fact]
        public async Task GetsPost_WithNonexistentId_Returns404NotFound()
        {
            // Arrange
            var expectedNotFoundId = 999;
            var expectedNotFoundResponseBody = "{}";

            // Act
            var response = await _postsService.GetPostByIdAsync(expectedNotFoundId);
            var statusCode = response.StatusCode;
            var responseBody = response.RawContent;

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
            Assert.Equal(expectedNotFoundResponseBody, responseBody);
        }

        // NOTE: JSONPlaceholder incorrectly returns 404 Not Found for negative IDs or zero.
        // In a production project, this is considered a bug.The server should validate
        // the input parameter and return 400 Bad Request with a structured error message.
        // Example of proper response: { "error": "Bad Request", "message": "Id must be greater than 0." }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetsPost_WithInvalidId_Returns404NotFound(int invalidId)
        {
            // Arrange
            var expectedResponseBody = "{}";

            // Act
            var response = await _postsService.GetPostByIdAsync(invalidId);
            var statusCode = response!.StatusCode;
            var responseBody = response.RawContent;

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
            Assert.Equal(expectedResponseBody, responseBody);
        }
    }
}
