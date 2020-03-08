using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain;

namespace Tickets.Repository
{
    public class TicketsRepository : RepositoryBase, ITicketsRepository
    {

        public TicketsRepository(TicketsContext context) : base(context)
        {
        }
        
        public async Task<Ticket> FindByIdAsync(int TicketId)
        {
            IQueryable<Ticket> query = _context.Tickets
                .Where(t => t.Id == TicketId);
            return await query.FirstOrDefaultAsync();   
        }

        public async Task<ICollection<Ticket>> GetAllTickets(int companyId)
        {
            return await _context.Tickets.Where(t => t.CompanyId == companyId).ToListAsync();
        }
    }
}