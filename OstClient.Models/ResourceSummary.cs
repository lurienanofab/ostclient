using Newtonsoft.Json;

namespace OstClient.Models
{
    public class ResourceSummary
    {
        [JsonProperty("resource_id")]
        public int ResourceID { get; set; }

        [JsonProperty("priority_urgency")]
        public int PriorityUrgency { get; set; }

        [JsonProperty("priority_desc")]
        public string PriorityDesc { get; set; }

        [JsonProperty("ticket_count")]
        public int TicketCount { get; set; }
    }
}
