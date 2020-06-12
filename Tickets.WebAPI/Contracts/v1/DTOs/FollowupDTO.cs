using System;
using Tickets.Domain;

namespace Tickets.WebAPI.Contracts.v1.DTOs
{
    public class FollowupDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long TicketId { get; set; }
        public int AuthorId { get; set; }
        public int CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}