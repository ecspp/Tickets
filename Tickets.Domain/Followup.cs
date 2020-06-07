using System;

namespace Tickets.Domain
{
    public class Followup {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}