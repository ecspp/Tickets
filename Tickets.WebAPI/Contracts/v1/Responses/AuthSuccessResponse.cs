namespace Tickets.WebAPI.Contracts.v1.Responses
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Username { get; set; }

    }
}