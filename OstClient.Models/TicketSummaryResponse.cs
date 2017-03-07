using Newtonsoft.Json;
using System.Collections.Generic;

namespace OstClient.Models
{
    public class TicketSummaryResponse
    {
        [JsonProperty("tickets")]
        public IEnumerable<TicketSummary> Tickets { get; set; }
    }
}
