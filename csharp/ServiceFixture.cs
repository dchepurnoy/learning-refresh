using TestTask2.Services;

namespace TestTask2
{
    public class ServiceFixture : IDisposable
    {
        private readonly HttpClient _client;
        public PostsService PostsService { get; }


        public ServiceFixture()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(TestConstants.BaseAddress),
                Timeout = TimeSpan.FromSeconds(5)
            };
            PostsService = new PostsService(_client);
            Console.WriteLine("[SETUP] ServiceFixture");
        }

        public void Dispose()
        {
            Console.WriteLine("[TEARDOWN] ServiceFixture");
            _client.Dispose();
        }
    }
}
