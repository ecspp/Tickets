using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain;
using Tickets.WebAPI.Data;

namespace Tickets.WebAPI.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly DataContext _dataContext;
        public CompanyService(DataContext dataContext)
        {
            this._dataContext = dataContext;

        }
        public async Task<Company> FindByEmailAsync(string email)
        {
            return await _dataContext.Companies.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}