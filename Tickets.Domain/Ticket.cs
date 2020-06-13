using System;
using System.Collections.Generic;

namespace Tickets.Domain
{
    public class Ticket
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public int? AuthorId { get; set; }
        public virtual User Author { get; set; }
        public virtual List<Followup> Followups { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<TicketContact> TicketContacts { get; set; }
    }
}