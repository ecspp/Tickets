namespace Tickets.WebAPI.Contracts.v1.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}