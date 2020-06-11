using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain;
using Tickets.WebAPI.Data;
using Tickets.WebAPI.Extensions;

namespace Tickets.WebAPI.Services
{
    public class TicketService : ITicketService
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAcessor;
        public TicketService(UserManager<User> userManager, DataContext dataContext, IHttpContextAccessor httpContextAcessor)
        {
            this._httpContextAcessor = httpContextAcessor;
            this._dataContext = dataContext;
            this._userManager = userManager;

        }
        public async Task<bool> CreateTicketAsync(Ticket ticket)
        {
            ticket.CreatedAt = DateTime.UtcNow;
            ticket.CompanyId = _httpContextAcessor.GetCompanyId();
            ticket.AuthorId = _httpContextAcessor.GetUserId();
            await _dataContext.Tickets.AddAsync(ticket);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteTicketAsync(int ticketId)
        {
            var ticket = await GetTicketByIdAsync(ticketId);
            _dataContext.Tickets.Remove(ticket);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<ICollection<Ticket>> GetAllTicketsAsync()
        {
            var companyId = _httpContextAcessor.GetCompanyId();
            return await _dataContext.Tickets.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            var companyId = _httpContextAcessor.GetCompanyId();
            return await _dataContext.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId && x.CompanyId == companyId);
        }

        public async Task<bool> UpdateTicketAsync(Ticket ticket)
        {
            ticket.UpdatedAt = DateTime.UtcNow;
            _dataContext.Tickets.Update(ticket);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

    }
}