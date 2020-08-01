using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tickets.WebAPI.Contracts.v1;
using Tickets.WebAPI.Contracts.v1.Requests;
using Tickets.WebAPI.Contracts.v1.Responses;
using Tickets.WebAPI.Services;

namespace Tickets.WebAPI.Controllers.v1
{
    [AllowAnonymous]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost(ApiRoutes.Auth.Register)]
        public async Task<ActionResult> Register([FromBody] RegistrationRequest registrationRequest)
        {
            var authResponse = await _authService.RegisterAsync(registrationRequest);

            if (!authResponse.Success)
            {
                foreach (var error in authResponse.Errors)
                {
                    ModelState.AddModelError("Register", error);
                }
                return ValidationProblem();
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken.ToString(),
                Username = authResponse.Username
            });
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var loginResult = await _authService.LoginAsync(loginRequest.Username, loginRequest.Password);

            if (!loginResult.Success)
            {
                foreach (var error in loginResult.Errors)
                {
                    ModelState.AddModelError("Login", error);
                }
                return ValidationProblem();
            }

            return Ok(new AuthSuccessResponse
            {
                Token = loginResult.Token,
                RefreshToken = loginResult.RefreshToken,
                Username = loginRequest.Username
            });
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequest refreshRequest)
        {
            var refreshResult = await _authService.RefreshTokenAsync(refreshRequest.Token, refreshRequest.RefreshToken);

            if (!refreshResult.Success)
            {
                foreach (var error in refreshResult.Errors)
                {
                    ModelState.AddModelError("Refresh", error);
                }
                return ValidationProblem();
            }

            return Ok(new AuthSuccessResponse
            {
                Token = refreshResult.Token,
                RefreshToken = refreshResult.RefreshToken
            });
        }
    }
}