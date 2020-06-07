using System.ComponentModel.DataAnnotations;

namespace Tickets.WebAPI.Contracts.v1.Requests
{
    public class ContactCreateRequest
    {
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}