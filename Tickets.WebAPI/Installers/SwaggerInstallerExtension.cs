using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Tickets.WebAPI.Installers
{
    public static class SwaggerInstallerExtension
    {
        public static void InstallSwagger(this IServiceCollection services) {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Tickets WebAPI",
                    Version = "v1"
                });
                c.AddSecurityDefinition(
                    "bearer",
                    new OpenApiSecurityScheme {
                        In = ParameterLocation.Header,
                        Description = "Bearer token...",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "bearer"},
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}