using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.Domain;

namespace Tickets.WebAPI.Services
{
    public interface IContactService
    {
        public Task<bool> CreateAsync(Contact contact);
        public Task<bool> UpdateAsync(Contact contact);
        public Task<bool> DeleteAsync(int contactId);
        public Task<Contact> GetByIdAsync(int contactId);
        public Task<ICollection<Contact>> GetAllAsync();
    }
}