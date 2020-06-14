using System.Collections.Generic;

namespace Tickets.WebAPI.Contracts.v1.Requests.Creation
{
    public class AddContactDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<int> ContactTypes { get; set; }
    }
}