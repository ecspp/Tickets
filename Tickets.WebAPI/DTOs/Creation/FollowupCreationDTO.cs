namespace Tickets.WebAPI.DTOs
{
    public class FollowupCreationDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TicketId { get; set; }
    }
}