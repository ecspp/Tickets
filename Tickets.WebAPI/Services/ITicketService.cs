using System.Collections;
using System;
using System.Threading.Tasks;
using Tickets.Domain;
using Tickets.WebAPI.Contracts.v1.Requests;
using Tickets.WebAPI.Contracts.v1.Responses;
using System.Collections.Generic;

namespace Tickets.WebAPI.Services
{
    public interface ITicketService
    {
         public Task<bool> CreateTicketAsync(Ticket ticket);
         public Task<bool> UpdateTicketAsync(Ticket ticket);
         public Task<bool> DeleteTicketAsync(Guid ticketId, Guid userId);
         public Task<Ticket> GetTicketByIdAsync(Guid ticketId , Guid companyId);
         public Task<IEnumerable<Ticket>> GetAllTicketsAsync(Guid userId);
    }
}