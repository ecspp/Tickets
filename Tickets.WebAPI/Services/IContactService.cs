using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tickets.Domain;
using Tickets.WebAPI.Contracts.v1.DTOs;
using Tickets.WebAPI.Contracts.v1.Requests.Creation;

namespace Tickets.WebAPI.Services
{
    public interface IContactService
    {
        public Task<bool> CreateAsync(Contact contact);
        public Task<Contact> CreateAsync(AddContactDTO dto);
        public Task<bool> UpdateAsync(ContactDTO contact, Contact entity);
        public Task<bool> DeleteAsync(int contactId);
        public Task<Contact> GetByIdAsync(int contactId);
        public Task<Contact> GetJoinedById(int id);
        public Task<ICollection<Contact>> GetAllAsync();
    }
}