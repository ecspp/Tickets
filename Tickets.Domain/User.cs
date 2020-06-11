using System;
using Microsoft.AspNetCore.Identity;

namespace Tickets.Domain
{
    public class User : IdentityUser<int>
    {
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }        
        public int? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}