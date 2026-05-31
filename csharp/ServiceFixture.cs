using TestTask2.Services;

namespace TestTask2
{
    public class ServiceFixture : IDisposable
    {
        private readonly HttpClient _client;
        public PostsService PostsService { get; }

        public ServiceFixture()
        {
            _client = new HttpClient();
            PostsService = new PostsService(_client);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
