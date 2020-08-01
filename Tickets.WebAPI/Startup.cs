using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using Tickets.WebAPI.Installers;
using Tickets.Domain.Options;

namespace Tickets.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallDatabase(Configuration);
            services.InstallServices(Configuration);
            services.InstallIdentityCore();
            services.InstallMvc();
            services.InstallValidationServices();
            services.AddAutoMapper(typeof(Startup));

            services.InstallSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            // app.UseHttpsRedirection();

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection("SwaggerOptions").Bind(swaggerOptions);

            app.UseSwagger(options => options.RouteTemplate = swaggerOptions.JsonRoute);
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });


            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            // app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyOrigin());
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // app.UseMvc();
            app.UseEndpoints(endpoins =>
            {
                endpoins.MapControllers();
                // endpoins.MapHealthChecks("/health");
            });
        }
    }
}
