using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.Domain;

namespace Tickets.WebAPI.Services
{
    public interface IContactService
    {
        public Task<bool> CreateContactAsync(Contact contact);
        public Task<bool> UpdateContactAsync(Contact contact);
        public Task<bool> DeleteContactAsync(Guid contactId, Guid userId);
        public Task<Contact> GetContactByIdAsync(Guid contactId, Guid companyId);
        public Task<IEnumerable<Contact>> GetAllContactsAsync(Guid userId);
    }
}