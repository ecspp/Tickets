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
    public class ContactService : IContactService
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAcessor;
        public ContactService(UserManager<User> userManager, DataContext dataContext, IHttpContextAccessor httpContextAcessor)
        {
            this._httpContextAcessor = httpContextAcessor;
            this._dataContext = dataContext;
            this._userManager = userManager;

        }
        public async Task<bool> CreateContactAsync(Contact contact)
        {
            await _dataContext.Contacts.AddAsync(contact);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteContactAsync(Guid contactId)
        {
            var contact = await GetContactByIdAsync(contactId);
            _dataContext.Contacts.Remove(contact);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<ICollection<Contact>> GetAllContactsAsync()
        {
            var companyId = _httpContextAcessor.GetCompanyId();
            return await _dataContext.Contacts.Where(x => x.CompanyId == companyId).ToListAsync();

        }

        public async Task<Contact> GetContactByIdAsync(Guid contactId)
        {
            var companyId = _httpContextAcessor.GetCompanyId();
            return await _dataContext.Contacts.FirstOrDefaultAsync(x => x.Id == contactId && x.CompanyId == companyId);
        }

        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            _dataContext.Contacts.Update(contact);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}