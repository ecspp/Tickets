using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain;

namespace Tickets.Repository
{
    public class CompanyRepository : RepositoryBase, ICompanyRepository
    {
        public CompanyRepository(TicketsContext context) : base(context)
        {
        }

        public async Task<Company> FindByIdAsync(int CompanyId)
        {
            IQueryable<Company> query = _context.Companies
                .Where(c => c.Id == CompanyId);
            return await query.FirstOrDefaultAsync();       
        }
    }
}