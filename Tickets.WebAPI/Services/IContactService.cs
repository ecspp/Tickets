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
        public Task<bool> DeleteContactAsync(int contactId);
        public Task<Contact> GetContactByIdAsync(int contactId);
        public Task<ICollection<Contact>> GetAllContactsAsync();
    }
}