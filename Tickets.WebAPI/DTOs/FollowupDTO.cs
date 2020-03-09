namespace Tickets.WebAPI.DTOs
{
    public class FollowupDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}