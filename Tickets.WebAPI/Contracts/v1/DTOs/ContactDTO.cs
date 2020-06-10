using System;

namespace Tickets.WebAPI.Contracts.v1.DTOs
{
    public class ContactDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid? CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}