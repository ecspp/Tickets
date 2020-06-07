using System;

namespace Tickets.Domain
{
    public class TicketsQueryData
    {
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
    }
}