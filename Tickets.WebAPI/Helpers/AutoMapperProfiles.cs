using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Tickets.Domain;
using Tickets.WebAPI.Contracts.v1.DTOs;
using Tickets.WebAPI.Contracts.v1.Requests.Creation;
using Tickets.WebAPI.Data;

namespace Tickets.WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            Configure();
        }

        private void Configure()
        {
            CreateMap<ContactType, ContactTypeDTO>().ReverseMap();
            CreateMap<Contact, ContactDTO>()
                .ForMember(dest => dest.ContactTypes, opt =>
                    opt.MapFrom(src => src.ContactTypeContacts.Select(x => x.ContactType))
                );
            CreateMap<Ticket, TicketDTO>().ReverseMap();
            CreateMap<Followup, FollowupDTO>().ReverseMap();

            CreateMap<AddContactDTO, Contact>();
            CreateMap<ContactDTO, Contact>();
        }
    }
}