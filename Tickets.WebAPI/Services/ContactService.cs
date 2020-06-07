using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tickets.Domain;
using Tickets.WebAPI.Data;

namespace Tickets.WebAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _dataContext;
        public ContactService(UserManager<User> userManager, DataContext dataContext)
        {
            this._dataContext = dataContext;
            this._userManager = userManager;

        }
        public async Task<bool> CreateContactAsync(Contact contact)
        {
            await _dataContext.Contacts.AddAsync(contact);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public Task<bool> DeleteContactAsync(Guid ContactId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contact>> GetAllContactsAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Contact> GetContactByIdAsync(Guid contactId, Guid companyId)
        {
            return await _dataContext.Contacts.FirstOrDefaultAsync(x => x.Id == contactId && x.CompanyId == companyId);
        }

        public Task<bool> UpdateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}