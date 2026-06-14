using System.Net;

namespace TestTask2.Models.Common
{
    public class ApiResponse<TSuccess>
    {
        public HttpStatusCode StatusCode { get; set; }
        public TSuccess? Data { get; set; }
        public string RawContent { get; set; } = string.Empty;
    }
}
