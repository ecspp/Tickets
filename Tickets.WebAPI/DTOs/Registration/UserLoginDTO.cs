using System.ComponentModel.DataAnnotations;

namespace Tickets.WebAPI.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        [MinLength(10)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}