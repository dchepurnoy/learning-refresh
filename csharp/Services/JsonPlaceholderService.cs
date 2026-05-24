using System.Text.Json;

namespace TestTask2.Services
{
    // ToDo: Add as service to test
    public class JsonPlaceholderService
    {
        private HttpClient _client;
        private string _url = "https://jsonplaceholder.typicode.com/posts/{0}";

        public JsonPlaceholderService() 
        {
            _client = new HttpClient();
        }

        public async Task<HttpResponseMessage> GetPostById(int id)
        {
            var url = string.Format(_url, id);
            HttpResponseMessage response = await _client.GetAsync(url);
            return response;
        }

        public async Task<T> GetDeserealizedContent<T>(HttpResponseMessage response)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var userData = JsonSerializer.Deserialize<T>(jsonResponse);
            return userData!;
        }
    }
}
