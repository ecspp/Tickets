using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.Domain;

namespace Tickets.WebAPI.Services
{
    public interface IContactTypeService
    {
         public Task<bool> CreateAsync(ContactType entity);
         public Task<bool> UpdateAsync(ContactType entity);
         public Task<bool> DeleteAsync(int id);
         public Task<ContactType> GetByIdAsync(int id);
         public Task<ICollection<ContactType>> GetAllAsync();
    }
}