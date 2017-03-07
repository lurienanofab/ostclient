using Newtonsoft.Json;
using OstClient.Models.Converters;
using System;

namespace OstClient.Models
{
    public class TicketMessage
    {
        [JsonProperty("msg_id")]
        public int MsgId { get; set; }

        [JsonProperty("created"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? Created { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("attachments")]
        public int Attachments { get; set; }
    }
}
