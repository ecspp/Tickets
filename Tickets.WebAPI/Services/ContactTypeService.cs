using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain;
using Tickets.WebAPI.Data;
using Tickets.WebAPI.Extensions;

namespace Tickets.WebAPI.Services
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly DataContext _db;
        private readonly IHttpContextAccessor _http;
        public ContactTypeService(DataContext db, IHttpContextAccessor http)
        {
            this._http = http;
            this._db = db;

        }
        public async Task<bool> CreateAsync(ContactType entity)
        {
            entity.CompanyId = _http.GetCompanyId();
            await _db.ContactTypes.AddAsync(entity);
            var created = await _db.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _db.ContactTypes.Remove(entity);
            var deleted = await _db.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<ICollection<ContactType>> GetAllAsync()
        {
            var companyId = _http.GetCompanyId();
            return await _db.ContactTypes.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<ContactType> GetByIdAsync(int id)
        {
            var companyId = _http.GetCompanyId();
            return await _db.ContactTypes.FirstOrDefaultAsync(x => x.Id == id && x.CompanyId == companyId);
        }

        public async Task<bool> UpdateAsync(ContactType entity)
        {
            _db.ContactTypes.Update(entity);
            var updated = await _db.SaveChangesAsync();
            return updated > 0;
        }
    }
}