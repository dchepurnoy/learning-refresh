using System.Net;
using System.Text.Json;
using TestTask2.Models;

namespace TestTask2
{
    public class HttpClientFixture : IDisposable
    {
        public HttpClient Client { get; } = new HttpClient();

        public void Dispose()
        {  Client.Dispose(); }
    }

    public class JsonPlaceholderTests : IClassFixture<HttpClientFixture>
    {
        private readonly HttpClient _client;

        public JsonPlaceholderTests(HttpClientFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GetReturnValidResponse()
        {
            int id = 1;
            var url = $"https://jsonplaceholder.typicode.com/posts/{id}";
            HttpResponseMessage response = await _client.GetAsync(url);
            var statusCode = response.StatusCode;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


            var jsonResponse = await response.Content.ReadAsStringAsync();
            PostResponse? userData = JsonSerializer.Deserialize<PostResponse>(jsonResponse)!;

            Assert.Multiple(
                () => Assert.True((int)userData.Id > 0, "Expected Id is null or empty"),
                () => Assert.True((int)userData.UserId > 0, "Expected UserId is null or empty"),
                () => Assert.False(string.IsNullOrEmpty(userData.Body), "Expected Body is null or empty"),
                () => Assert.False(string.IsNullOrEmpty(userData.Title), "Expected Title is null or empty")
                );
            Assert.True(userData.Id == id);
        }
    }
}
