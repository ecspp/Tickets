using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Tickets.WebAPI.Contracts.v1.DTOs;
using Tickets.WebAPI.Contracts.v1.DTOs.Validation;
using Tickets.WebAPI.Contracts.v1.Requests.Creation;
using Tickets.WebAPI.Contracts.v1.Requests.Creation.Validation;

namespace Tickets.WebAPI.Installers
{
    public static class ValidationServicesInstaller
    {
        public static void InstallValidationServices(this IServiceCollection services)
        {
            services.AddTransient<IValidator<AddFollowupDTO>, AddFollowupDtoValidator>();
            
            services.AddTransient<IValidator<TicketDTO>, TicketDtoValidator>();
            services.AddTransient<IValidator<AddTicketDTO>, AddTicketDtoValidator>();

            services.AddTransient<IValidator<AddContactTypeDTO>, AddContactTypeValidator>();
            services.AddTransient<IValidator<ContactTypeDTO>, ContactTypeDtoValidator>();
        }
    }
}