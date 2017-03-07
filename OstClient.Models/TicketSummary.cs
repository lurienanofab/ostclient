using Newtonsoft.Json;
using OstClient.Models.Converters;
using System;

namespace OstClient.Models
{
    public class TicketSummary
    {
        [JsonProperty("ticket_id")]
        public int Id { get; set; }

        [JsonProperty("ticketID")]
        public string TicketID { get; set; }

        [JsonProperty("dept_id")]
        public int DeptId { get; set; }

        [JsonProperty("priority_id")]
        public int PriorityId { get; set; }

        [JsonProperty("topic_id")]
        public int TopicId { get; set; }

        [JsonProperty("staff_id")]
        public int StaffId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("helptopic")]
        public string HelpTopic { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("phone_ext")]
        public string PhoneExt { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("isoverdue"), JsonConverter(typeof(BooleanStringConverter))]
        public bool IsOverdue { get; set; }

        [JsonProperty("isanswered"), JsonConverter(typeof(BooleanStringConverter))]
        public bool IsAnswered { get; set; }

        [JsonProperty("duedate"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? DueDate { get; set; }

        [JsonProperty("reopened"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? Reopened { get; set; }

        [JsonProperty("closed"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? Closed { get; set; }

        [JsonProperty("lastmessage"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? LastMessage { get; set; }

        [JsonProperty("lastresponse"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? LastResponse { get; set; }

        [JsonProperty("created"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? Created { get; set; }

        [JsonProperty("updated"), JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? Updated { get; set; }

        [JsonProperty("resource_id")]
        public int ResourceID { get; set; }

        [JsonProperty("priority_desc")]
        public string PriorityDescription { get; set; }

        [JsonProperty("priority_urgency")]
        public int PriorityUrgency { get; set; }

        [JsonProperty("assigned_to")]
        public string AssignedTo { get; set; }
    }
}
