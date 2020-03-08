namespace Tickets.WebAPI.DTOs
{
    public class TicketDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
    }
}