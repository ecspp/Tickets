using System;
using System.Threading.Tasks;
using Tickets.Domain;
using System.Collections.Generic;

namespace Tickets.WebAPI.Services
{
    public interface ITicketService
    {
         public Task<bool> CreateTicketAsync(Ticket ticket);
         public Task<bool> UpdateTicketAsync(Ticket ticket);
         public Task<bool> DeleteTicketAsync(int ticketId);
         public Task<Ticket> GetTicketByIdAsync(int ticketId);
         public Task<ICollection<Ticket>> GetAllTicketsAsync();
    }
}