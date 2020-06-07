using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain;
using Tickets.WebAPI.Contracts.v1.Requests;
using Tickets.WebAPI.Contracts.v1.Responses;
using Tickets.WebAPI.Data;

namespace Tickets.WebAPI.Services
{
    public class TicketService : ITicketService
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _dataContext;
        public TicketService(UserManager<User> userManager, DataContext dataContext)
        {
            this._dataContext = dataContext;
            this._userManager = userManager;

        }
        public async Task<bool> CreateTicketAsync(Ticket ticket)
        {
            await _dataContext.Tickets.AddAsync(ticket);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteTicketAsync(Guid ticketId, Guid userId)
        {
            var ticket = await GetTicketByIdAsync(ticketId, userId);
            _dataContext.Tickets.Remove(ticket);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user != null) {
                return await _dataContext.Tickets.Where(x => x.UserId == userId && x.CompanyId == user.CompanyId).ToListAsync();
            }
            return Enumerable.Empty<Ticket>();
        }

        public async Task<Ticket> GetTicketByIdAsync(Guid ticketId, Guid companyId)
        {
            return await _dataContext.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId && x.UserId == companyId);
        }

        public async Task<bool> UpdateTicketAsync(Ticket ticket)
        {
            _dataContext.Tickets.Update(ticket);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}