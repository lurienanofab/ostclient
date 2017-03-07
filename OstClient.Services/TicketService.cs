using OstClient.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Mail;

namespace OstClient.Services
{
    public class TicketService
    {
        private readonly HttpClient _httpClient;

        public ServiceConfiguration Configuration { get; }

        public TicketService() : this(ServiceConfiguration.Current) { }

        public TicketService(ServiceConfiguration config)
        {
            Configuration = config;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = Configuration.BaseAddress;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", config.ApiKey);
        }

        public async Task<TicketSummaryResponse> SelectTicketsByResource(int resourceId)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("action", "select-tickets-by-resource");
            dict.Add("resource_id", resourceId.ToString());
            dict.Add("status", "open");
            dict.Add("format", "json");

            FormUrlEncodedContent content = new FormUrlEncodedContent(dict);

            var msg = await _httpClient.PostAsync("data-exec.php", content);
            var result = await msg.Content.ReadAsAsync<TicketSummaryResponse>();
            return result;
        }

        public async Task<TicketSummaryResponse> SelectTicketsByEmail(string email)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("action", "select-tickets-by-email");
            dict.Add("email", email);
            dict.Add("status", "open");
            dict.Add("format", "json");

            FormUrlEncodedContent content = new FormUrlEncodedContent(dict);

            var msg = await _httpClient.PostAsync("data-exec.php", content);
            var result = await msg.Content.ReadAsAsync<TicketSummaryResponse>();
            return result;
        }

        public async Task<TicketDetailResponse> SelectTicketDetail(string ticketID)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("action", "ticket-detail");
            dict.Add("ticketID", ticketID);
            dict.Add("format", "json");

            FormUrlEncodedContent content = new FormUrlEncodedContent(dict);

            var msg = await _httpClient.PostAsync("data-exec.php", content);
            var result = await msg.Content.ReadAsAsync<TicketDetailResponse>();
            return result;
        }

        public async Task<TicketDetailResponse> PostMessage(string ticketID, string message)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("action", "post-message");
            dict.Add("ticketID", ticketID);
            dict.Add("message", message);
            dict.Add("format", "json");

            FormUrlEncodedContent content = new FormUrlEncodedContent(dict);

            var msg = await _httpClient.PostAsync("data-exec.php", content);
            var result = await msg.Content.ReadAsAsync<TicketDetailResponse>();
            return result;
        }

        public async Task<TicketSummaryResponse> AddTicket(AddTicketArgs args)
        {
            if (!string.IsNullOrEmpty(args.Cc))
            {
                using (SmtpClient client = new SmtpClient(Configuration.SmtpHost))
                using (MailMessage mm = new MailMessage(Configuration.SystemEmail, args.Cc, args.Subject, args.Message))
                    client.Send(mm);
            }

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("action", "add-ticket");
            dict.Add("resource_id", args.ResourceID.ToString());
            dict.Add("email", args.Email);
            dict.Add("name", args.Name);
            dict.Add("queue", args.Queue);
            dict.Add("subject", args.Subject);
            dict.Add("message", args.Message);
            dict.Add("pri", args.Priority);
            dict.Add("search", TicketSearchTypeUtil.ToString(args.Search));
            dict.Add("format", "json");

            FormUrlEncodedContent content = new FormUrlEncodedContent(dict);

            var msg = await _httpClient.PostAsync("data-exec.php", content);
            var result = await msg.Content.ReadAsAsync<TicketSummaryResponse>();
            return result;
        }

        public async Task<ResourceSummaryResponse> GetSummary(IEnumerable<int> resources)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("action", "summary");
            dict.Add("resources", string.Join(",", resources));
            dict.Add("format", "json");

            FormUrlEncodedContent content = new FormUrlEncodedContent(dict);

            var msg = await _httpClient.PostAsync("data-exec.php", content);
            var result = await msg.Content.ReadAsAsync<ResourceSummaryResponse>();
            return result;
        }
    }
}
