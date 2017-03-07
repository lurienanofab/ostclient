using Newtonsoft.Json;
using OstClient.Models.Converters;
using System;

namespace OstClient.Models
{
    public class TicketInfo
    {
        [JsonProperty("ticketID")]
        public string TicketID { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("dept_name")]
        public string DeptName { get; set; }

        [JsonProperty("created"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? Created { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("assigned_name")]
        public string AssignedName { get; set; }

        [JsonProperty("assigned_email")]
        public string AssignedEmail { get; set; }

        [JsonProperty("help_topic")]
        public string HelpTopic { get; set; }

        [JsonProperty("last_response"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? LastResponse { get; set; }

        [JsonProperty("last_message"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? LastMessage { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("due_date"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? DueDate { get; set; }
    }
}
