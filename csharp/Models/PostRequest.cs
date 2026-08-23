using System.Text.Json.Serialization;

namespace TestTask2.Models
{
    public class PostRequest
    {
        [JsonPropertyName("userId")]
        public int? UserId { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("body")]
        public string? Body { get; set; }
    }
}
