using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Tickets.Domain;
using Tickets.WebAPI.Data;

namespace Tickets.WebAPI.Installers
{
    public static class IdentityCoreInstallerExtension
    {
        public static void InstallIdentityCore(this IServiceCollection services)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();

        }
    }
}