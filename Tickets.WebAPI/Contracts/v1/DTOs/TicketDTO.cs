using System;

namespace Tickets.WebAPI.Contracts.v1.DTOs
{
    public class TicketDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}