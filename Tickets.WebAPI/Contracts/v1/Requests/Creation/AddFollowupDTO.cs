
namespace Tickets.WebAPI.Contracts.v1.Requests.Creation
{
    public class AddFollowupDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long TicketId { get; set; }
    }
}