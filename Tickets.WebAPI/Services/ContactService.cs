using System.Collections;
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
using AutoMapper;
using Tickets.WebAPI.Contracts.v1.Requests.Creation;
using Tickets.WebAPI.Contracts.v1.DTOs;

namespace Tickets.WebAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _db;
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;
        public ContactService(UserManager<User> userManager, DataContext dataContext, IHttpContextAccessor httpContextAcessor, IMapper mapper)
        {
            this._mapper = mapper;
            this._http = httpContextAcessor;
            this._db = dataContext;
            this._userManager = userManager;

        }
        public async Task<bool> CreateAsync(Contact contact)
        {
            contact.CompanyId = _http.GetCompanyId();
            contact.CreatedAt = DateTime.UtcNow;
            await _db.Contacts.AddAsync(contact);
            var created = await _db.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Contact> CreateAsync(AddContactDTO dto)
        {
            var newContact = _mapper.Map<Contact>(dto);
            var created = await CreateAsync(newContact);

            if (created && dto.ContactTypeIds.Count > 0)
            {
                List<ContactTypeContact> types = new List<ContactTypeContact>();
                foreach (var typeId in dto.ContactTypeIds)
                {
                    types.Add(new ContactTypeContact
                    {
                        ContactId = newContact.Id,
                        ContactTypeId = typeId
                    });
                }
                _db.ContactTypeContacts.AddRange(types);
                await _db.SaveChangesAsync();
            }

            var newContactLoaded = await _db.Contacts
                .Include(x => x.ContactTypeContacts)
                .ThenInclude(x => x.ContactType)
                .Where(x => x.Id == newContact.Id).FirstOrDefaultAsync();

            return created ? newContactLoaded : null;
        }

        public async Task<bool> DeleteAsync(int contactId)
        {
            var contact = await GetByIdAsync(contactId);
            _db.Contacts.Remove(contact);
            var deleted = await _db.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<ICollection<Contact>> GetAllAsync()
        {
            var companyId = _http.GetCompanyId();
            return await _db.Contacts.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int contactId)
        {
            var companyId = _http.GetCompanyId();
            return await _db.Contacts.FirstOrDefaultAsync(x => x.Id == contactId && x.CompanyId == companyId);
        }

        public async Task<Contact> GetJoinedById(int id)
        {
            var companyId = _http.GetCompanyId();
            return await _db.Contacts
                .Include(c => c.ContactTypeContacts)
                    .ThenInclude(ctc => ctc.ContactType)
                .Where(c => c.Id == id && c.CompanyId == companyId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(ContactDTO dto, Contact entity)
        {
            _mapper.Map<ContactDTO, Contact>(dto, entity);
            _db.Contacts.Update(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
            entity.UpdatedAt = DateTime.UtcNow;
            var updated = await _db.SaveChangesAsync();
            await AddOrUpdateContactTypes(dto, entity);
            return updated > 0;
        }

        private async Task AddOrUpdateContactTypes(ContactDTO dto, Contact entity)
        {
            var existingContactTypes = _db.ContactTypeContacts.Where(x => x.ContactId == entity.Id).ToList();
            var typesInDto = dto.ContactTypes.ToList();
            var remove = new List<ContactTypeContact>();
            var add = new List<ContactTypeContact>();
            foreach (var ctc in existingContactTypes)
            {
                if (typesInDto.FindIndex(x => x.Id == ctc.ContactTypeId) < 1)
                {
                    remove.Add(ctc);
                }
            }

            foreach (var ctd in typesInDto)
            {
                if (existingContactTypes.FindIndex(x => x.ContactTypeId == ctd.Id) < 1)
                {
                    var newCtc = new ContactTypeContact
                    {
                        ContactTypeId = ctd.Id,
                        ContactId = entity.Id
                    };
                    add.Add(newCtc);
                }
            }
            if (remove.Count > 0)
            {
                _db.ContactTypeContacts.RemoveRange(remove);
            }
            if (add.Count > 0)
            {
                _db.ContactTypeContacts.AddRange(add);
            }
            var changed = await _db.SaveChangesAsync();
        }
    }
}