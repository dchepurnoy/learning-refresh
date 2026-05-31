using System.Net;
using System.Text.Json;
using TestTask2.Models;

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

        public async Task<(HttpStatusCode, PostResponse?)> GetPostByIdAsync(int id)
        {
            if (id <= 0)
            {
                return (HttpStatusCode.BadRequest, null);
            }

            string relativePath = $"posts/{id}";
            HttpResponseMessage response = await _client.GetAsync(relativePath);

            if (!response.IsSuccessStatusCode) 
            { 
                return (response.StatusCode, null); 
            }

            PostResponse? responseData = await GetDeserializedContent<PostResponse?>(response);
            return (response.StatusCode, responseData);
        }

        private async Task<T?> GetDeserializedContent<T>(HttpResponseMessage response)
        {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            T? userData = JsonSerializer.Deserialize<T?>(jsonResponse);
            return userData;
        }
    }
}
