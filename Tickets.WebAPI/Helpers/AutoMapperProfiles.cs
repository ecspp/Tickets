using AutoMapper;
using Tickets.Domain;
using Tickets.WebAPI.Contracts.v1.DTOs;

namespace Tickets.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<Ticket, TicketDTO>().ReverseMap();
        }
    }
}