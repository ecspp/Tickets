using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tickets.Domain;
using Tickets.WebAPI.Contracts.v1.Requests;
using Tickets.WebAPI.Data;
using Tickets.Domain.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Tickets.WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _dataContext;
        private readonly ICompanyService _companyService;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public AuthService(UserManager<User> userManager, DataContext dataContext, ICompanyService companyService, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters)
        {
            this._tokenValidationParameters = tokenValidationParameters;
            this._jwtSettings = jwtSettings;
            this._companyService = companyService;
            this._dataContext = dataContext;
            this._userManager = userManager;

        }
        public async Task<AuthenticationResult> RegisterAsync(RegistrationRequest registrationRequest)
        {
            if (await CheckIfUserExistsByEmail(registrationRequest.User.Email))
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this e-mail already exists" }
                };
            }
            if (await CheckIfUserExistsByUserName(registrationRequest.User.UserName))
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this username already exists" }
                };
            }
            if (await CheckIfCompanyExistsByEmail(registrationRequest.Company.Email))
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Company with this email already exists" }
                };
            }

            var newCompany = new Company{
                CorporateName = registrationRequest.Company.CorporateName,
                Email = registrationRequest.Company.Email
            };

            _dataContext.Companies.Add(newCompany);
            var created = await _dataContext.SaveChangesAsync();

            var newUser = new User
            {
                UserName = registrationRequest.User.UserName,
                Email = registrationRequest.User.Email,
                Company = newCompany
            };

            var createdUser = await _userManager.CreateAsync(newUser, registrationRequest.User.Password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }
            return await AuthenticateUserAsync(newUser);
        }

        public async Task<AuthenticationResult> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User doesn't exist"}
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User or password is wrong"}
                };
            }

            return await AuthenticateUserAsync(user);
        }
        
        private async Task<AuthenticationResult> AuthenticateUserAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id.ToString()),
                    new Claim("companyId", user.CompanyId.ToString())
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _dataContext.RefreshTokens.AddAsync(refreshToken);
            await _dataContext.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token.ToString(),
                Username = user.UserName
            };
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, Guid refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);
            if (validatedToken == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Invalid token" }
                };
            }

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.Now)
            {
                return new AuthenticationResult { Errors = new[] { "This token hasn'r expired yet" }};
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _dataContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshToken == null)
            {
                return new AuthenticationResult { Errors = new[] {"This refresh token doesn't exist"}};
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult { Errors = new[] {"This refresh token has expired"}};
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult { Errors = new[] {"This refresh token has been invalidated"}};
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult { Errors = new[] {"This refresh token has been used"}};
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult { Errors = new[] {"This refresh token has doesn't match this JWT"}};
            }

            storedRefreshToken.Used = true;
            _dataContext.RefreshTokens.Update(storedRefreshToken);
            await _dataContext.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);

            return await AuthenticateUserAsync(user);
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAm(validatedToken)) 
                {
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) && 
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<bool> CheckIfUserExistsByEmail(string email)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            return (existingUser != null);
        }

        private async Task<bool> CheckIfUserExistsByUserName(string userName)
        {
            var existingUser = await _userManager.FindByNameAsync(userName);
            return (existingUser != null);
        }

        private async Task<bool> CheckIfCompanyExistsByEmail(string email)
        {
            var existingCompany = await _companyService.FindByEmailAsync(email);
            return (existingCompany != null);
        }
    }
}