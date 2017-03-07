using Newtonsoft.Json;

namespace OstClient.Models
{
    public class TicketDetailResponse
    {
        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("detail")]
        public TicketDetail Detail { get; set; }
    }
}
