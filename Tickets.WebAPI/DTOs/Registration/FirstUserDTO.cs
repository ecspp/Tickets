

using Tickets.DataAnnotations;

namespace Tickets.WebAPI.DTOs
{
    public class FirstUserDTO
    {
        [UserNameAttribute]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}