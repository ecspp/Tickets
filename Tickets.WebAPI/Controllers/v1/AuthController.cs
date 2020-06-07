using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tickets.WebAPI.Contracts.v1;
using Tickets.WebAPI.Contracts.v1.Requests;
using Tickets.WebAPI.Contracts.v1.Responses;
using Tickets.WebAPI.Services;

namespace Tickets.WebAPI.Controllers.v1
{
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
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var authResponse = await _authService.RegisterAsync(registrationRequest);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken.ToString()
            });
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var loginResult = await _authService.LoginAsync(loginRequest.Email, loginRequest.Password);

            if (!loginResult.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = new[] { "User or password is wrong" }
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = loginResult.Token,
                RefreshToken = loginResult.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        public async Task<ActionResult> Refresh([FromBody] RefreshTokenRequest refreshRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }

            var refreshResult = await _authService.RefreshTokenAsync(refreshRequest.Token, refreshRequest.RefreshToken);

            if (!refreshResult.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = refreshResult.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = refreshResult.Token,
                RefreshToken = refreshResult.RefreshToken
            });
        }
    }
}