using System.Collections.Generic;
using Tickets.Domain.Identity;

namespace Tickets.Domain
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<User> Users { get; set; }
    }
}