using Newtonsoft.Json;
using OstClient.Models.Converters;
using System;

namespace OstClient.Models
{
    public class TicketResponse
    {
        [JsonProperty("response_id")]
        public int ResponseId { get; set; }

        [JsonProperty("msg_id")]
        public int MsgId { get; set; }

        [JsonProperty("staff_id")]
        public int StaffId { get; set; }

        [JsonProperty("staff_name")]
        public string StaffName { get; set; }

        [JsonProperty("response")]
        public string Response { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("created"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? Created { get; set; }

        [JsonProperty("attachments")]
        public int Attachments { get; set; }
    }
}
