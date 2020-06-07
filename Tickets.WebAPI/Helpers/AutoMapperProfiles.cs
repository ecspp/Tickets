using AutoMapper;
using Tickets.Domain;
using Tickets.WebAPI.Contracts.v1.Requests;

namespace Tickets.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // CreateMap<CompanyUserRegistrationDTO, User>();
            // CreateMap<CompanyRegistrationDTO, Company>();
            // TODO: CREATE MAPER TO NEW CONTRACTS
            // CreateMap<CompanyRegistrationDTO, Company>().ReverseMap();
            // CreateMap<FirstUserDTO, User>().ReverseMap();
            // CreateMap<UserDTO, User>().ReverseMap();
            // CreateMap<CompanyDTO, Company>().ReverseMap();
            // CreateMap<TicketDTO, Ticket>().ReverseMap();
        }
    }
}