using Newtonsoft.Json;
using System.Collections.Generic;

namespace OstClient.Models
{
    public class TicketDetail
    {
        [JsonProperty("info")]
        public TicketInfo Info { get; set; }

        [JsonProperty("messages")]
        public IEnumerable<TicketMessage> Messages { get; set; }

        [JsonProperty("responses")]
        public IEnumerable<TicketResponse> Responses { get; set; }
    }
}
