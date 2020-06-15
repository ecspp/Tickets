using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Tickets.Domain.Options;
using Tickets.WebAPI.Contracts.v1.Requests.Creation;
using Tickets.WebAPI.Contracts.v1.Requests.Creation.Validation;
using Tickets.WebAPI.Services;

namespace Tickets.WebAPI.Installers
{
    public static class ServicesInstallerExtension
    {
        public static void InstallServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IFollowupService, FollowupService>();
            services.AddScoped<IContactTypeService, ContactTypeService>();
            
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            // services.AddControllers();
            
        }
    }
}