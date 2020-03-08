using AutoMapper;
using Tickets.Domain;
using Tickets.Domain.Identity;
using Tickets.WebAPI.DTOs;

namespace Tickets.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CompanyRegistrationDTO, Company>().ReverseMap();
            CreateMap<FirstUserDTO, User>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<CompanyDTO, Company>().ReverseMap();
            CreateMap<TicketDTO, Ticket>().ReverseMap();
        }
    }
}