using System;
using System.Threading.Tasks;
using Tickets.Domain;
using Tickets.WebAPI.Contracts.v1.Requests;

namespace Tickets.WebAPI.Services
{
    public interface IAuthService
    {
         public Task<AuthenticationResult> RegisterAsync(RegistrationRequest registrationRequest);
         public Task<AuthenticationResult> LoginAsync(string email, string password);
         public Task<AuthenticationResult> RefreshTokenAsync(string token, Guid refreshToken);

    }
}