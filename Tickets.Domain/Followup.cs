using System;

namespace Tickets.Domain
{
    public class Followup {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}