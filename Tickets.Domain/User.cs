using System;
using Microsoft.AspNetCore.Identity;

namespace Tickets.Domain
{
    public class User : IdentityUser<Guid>
    {
        public Guid? CompanyId { get; set; }
        public virtual Company Company { get; set; }        
        public Guid? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
    }
}