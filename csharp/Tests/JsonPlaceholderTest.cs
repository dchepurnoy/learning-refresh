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

        [Fact]
        public async Task GetReturnsValidResponseAsync()
        {
            var expectedId = 1;
            var (statusCode, responseData) = await _postsService.GetPostByIdAsync(expectedId);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.NotNull(responseData);

            Assert.Multiple(
                () => Assert.True((int)responseData.Id > 0, "Expected Id is null or empty"),
                () => Assert.True((int)responseData.UserId > 0, "Expected UserId is null or empty"),
                () => Assert.False(string.IsNullOrEmpty(responseData.Body), "Expected Body is null or empty"),
                () => Assert.False(string.IsNullOrEmpty(responseData.Title), "Expected Title is null or empty")
                );
            Assert.True(responseData.Id == expectedId, "Response Id is not as expected");
        }
    }
}
