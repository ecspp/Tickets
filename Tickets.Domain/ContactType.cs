using System.Collections.Generic;

namespace Tickets.Domain
{
    public class ContactType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<ContactTypeContact> ContactTypeContacts { get; set; }
    }
}