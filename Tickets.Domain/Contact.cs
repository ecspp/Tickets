using System;
using System.Collections.Generic;

namespace Tickets.Domain
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<TicketContact> TicketContacts { get; set; }
    }
}