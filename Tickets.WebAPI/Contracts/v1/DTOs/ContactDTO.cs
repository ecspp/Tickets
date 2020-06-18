using System;
using System.Collections;
using System.Collections.Generic;
using Tickets.Domain;

namespace Tickets.WebAPI.Contracts.v1.DTOs
{
    public class ContactDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<ContactTypeDTO> ContactTypes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}