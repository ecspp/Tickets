using System.Threading.Tasks;
using Tickets.Domain;

namespace Tickets.WebAPI.Services
{
    public interface ICompanyService
    {
         public Task<Company> FindByEmailAsync(string email);
        //  public Task<Company> -->> CNPJ, etc
    }
}