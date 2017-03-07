using Newtonsoft.Json;

namespace OstClient.Models
{
    public class AddTicketArgs
    {
        [JsonProperty("resource_id")]
        public int ResourceID { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("queue")]
        public string Queue { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("pri")]
        public string Priority { get; set; }

        [JsonProperty("cc")]
        public string Cc { get; set; }

        [JsonProperty("search")]
        public TicketSearchType Search { get; set; }
    }
}