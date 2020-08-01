using System;
using System.Collections.Generic;

namespace Tickets.Domain
{
    public class Company
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public virtual List<User> Users { get; set; }
        public string CpfCnpj { get; set; }
        public string CorporateName { get; set; }

    }
}