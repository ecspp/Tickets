namespace Tickets.WebAPI.Contracts.v1.Responses
{
    public class TicketDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}