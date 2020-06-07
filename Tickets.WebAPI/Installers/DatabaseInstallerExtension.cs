using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tickets.WebAPI.Data;

namespace Tickets.WebAPI.Installers
{
    public static class DatabaseInstallerExtension
    {
        public static void InstallDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseLazyLoadingProxies()
                .UseNpgsql(configuration.GetConnectionString("Postgres"));
            });
        }
    }
}