using System.Threading.Tasks;
using Tickets.Domain;

namespace Tickets.Repository
{
    public interface ICompanyRepository : IRepositoryBase
    {
         Task<Company> FindByIdAsync(int CompanyId);
    }
}