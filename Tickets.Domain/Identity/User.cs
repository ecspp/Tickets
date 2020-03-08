using Microsoft.AspNetCore.Identity;

namespace Tickets.Domain.Identity
{
    public class User : IdentityUser<int>
    {

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}