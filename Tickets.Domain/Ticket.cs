using Tickets.Domain.Identity;

namespace Tickets.Domain
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}