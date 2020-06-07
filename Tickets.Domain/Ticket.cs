using System;
using System.Collections.Generic;

namespace Tickets.Domain
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Followup> Followups { get; set; }
    }
}