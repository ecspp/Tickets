using System;
using System.Collections.Generic;

namespace Tickets.Domain
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public virtual List<User> Users { get; set; }
    }
}