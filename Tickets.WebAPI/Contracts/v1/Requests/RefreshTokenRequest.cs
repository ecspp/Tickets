using System;

namespace Tickets.WebAPI.Contracts.v1.Requests
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public Guid RefreshToken { get; set; }
    }
}