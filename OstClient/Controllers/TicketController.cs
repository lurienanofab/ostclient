using OstClient.Models;
using OstClient.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace OstClient.Controllers
{
    public class TicketController : ApiController
    {
        [Route("api/ticket")]
        public async Task<TicketSummaryResponse> GetTickets(string email)
        {
            var service = new TicketService();
            var result = await service.SelectTicketsByEmail(email);
            return result;
        }

        [Route("api/ticket")]
        public async Task<TicketSummaryResponse> GetTickets(int resourceId)
        {
            var service = new TicketService();
            var result = await service.SelectTicketsByResource(resourceId);
            return result;
        }

        [Route("api/ticket/{ticketID}")]
        public async Task<TicketDetailResponse> GetTicketDetail(string ticketID)
        {
            var service = new TicketService();
            var result = await service.SelectTicketDetail(ticketID);
            return result;
        }

        [Route("api/ticket/{ticketID}/message")]
        public async Task<TicketDetailResponse> PostMessage([FromBody] string message, [FromUri] string ticketID)
        {
            var service = new TicketService();
            var result = await service.PostMessage(ticketID, message);
            return result;
        }

        [HttpPost, Route("api/ticket")]
        public async Task<TicketSummaryResponse> AddTicket([FromBody] AddTicketArgs args)
        {
            var service = new TicketService();
            var result = await service.AddTicket(args);
            return result;
        }

        [HttpPost, Route("api/ticket/resource-summary")]
        public async Task<ResourceSummaryResponse> ResourceSummary([FromBody] int[] resources)
        {
            var service = new TicketService();
            var result = await service.GetSummary(resources);
            return result;
        }
    }
}
