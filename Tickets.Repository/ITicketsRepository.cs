using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.Domain;

namespace Tickets.Repository
{
    public interface ITicketsRepository : IRepositoryBase
    {
         public Task<Ticket> FindByIdAsync(int TicketId);
         public Task<ICollection<Ticket>> GetAllTickets(int companyId);
    }
}