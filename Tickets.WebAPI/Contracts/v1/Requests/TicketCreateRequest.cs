namespace Tickets.WebAPI.Contracts.v1.Requests
{
    public class TicketCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}