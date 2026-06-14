using System.Net;
using System.Text.Json;
using TestTask2.Models;
using TestTask2.Models.Common;

namespace TestTask2.Services
{
    public class PostsService 
    {
        private readonly HttpClient _client;
        private readonly string _baseAddress = "https://jsonplaceholder.typicode.com/";

        public PostsService(HttpClient client) 
        {
            _client = client;
            _client.BaseAddress = new Uri(_baseAddress);
        }

        public async Task<ApiResponse<PostResponse>> GetPostByIdAsync(int id)
        {
            string relativePath = $"posts/{id}";
            HttpResponseMessage response = await _client.GetAsync(relativePath);

            string rawJson = await response.Content.ReadAsStringAsync();

            var apiResponse = new ApiResponse<PostResponse>()
            {
                StatusCode = response.StatusCode,
                RawContent = rawJson
            };

            if (response.StatusCode == HttpStatusCode.OK) 
            {
                apiResponse.Data = await GetDeserializedContent<PostResponse>(rawJson);
            }

            return apiResponse;
        }

        private async Task<T?> GetDeserializedContent<T>(string jsonResponse)
        {
            T? userData = JsonSerializer.Deserialize<T?>(jsonResponse);
            return userData;
        }
    }
}
