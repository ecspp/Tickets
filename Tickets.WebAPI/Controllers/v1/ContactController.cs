using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickets.Domain;
using Tickets.WebAPI.Contracts.v1;
using Tickets.WebAPI.Contracts.v1.Requests;
using Tickets.WebAPI.Contracts.v1.Responses;
using Tickets.WebAPI.Services;

namespace Tickets.WebAPI.Controllers.v1
{
    [Authorize]
    [ApiController]
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        public ContactController(IContactService contactService, IMapper mapper)
        {
            this._mapper = mapper;
            this._contactService = contactService;

        }

        [HttpPost(ApiRoutes.Contact.Create)]
        public async Task<ActionResult> Create([FromBody] ContactCreateRequest createRequest)
        {
            var newContact = new Contact
            {
                Name = createRequest.Name,
                Email = createRequest.Email,
                CompanyId = GetCompanyId()
            };

            var created = await _contactService.CreateContactAsync(newContact);
            if (!created)
            {
                ModelState.AddModelError("Contact", "It was not possible to create the contact");
                return ValidationProblem();
            }

            return Ok(new ContactCreateResponse
            {
                Succes = true,
                Id = newContact.Id.ToString(),
                Name = newContact.Name
            });
        }

        [HttpPut(ApiRoutes.Contact.Update)]
        public async Task<ActionResult> Update([FromRoute] Guid contactId, [FromBody] ContactUpdateRequest updateRequest)
        {
            var contact = await _contactService.GetContactByIdAsync(contactId);
            if (contact == null)
            {
                ModelState.AddModelError("Contact", "Contact not found");
                return ValidationProblem();
            }

            contact.Name = updateRequest.Name;
            contact.Email = updateRequest.Email;

            var updated = await _contactService.UpdateContactAsync(contact);
            if (!updated)
            {
                ModelState.AddModelError("Contact", "An error ocurred while updating the contact");
                return ValidationProblem();
            }

            return Ok(new ContactCreateResponse
            {
                Name = contact.Name,
                Email = contact.Email
            });
        }

        [HttpGet(ApiRoutes.Contact.Get)]
        public async Task<ActionResult> GetAction([FromRoute] Guid contactId)
        {
            var contact = await _contactService.GetContactByIdAsync(contactId);

            if (contact == null)
            {
                ModelState.AddModelError("Contact", "Contact not found");
                return ValidationProblem();
            }

            return Ok(new ContactDTO
            {
                Name = contact.Name,
                Email = contact.Email
            });
        }

        [HttpDelete(ApiRoutes.Contact.Delete)]
        public async Task<ActionResult> Delete([FromRoute] Guid contactId)
        {
            var deleted = await _contactService.DeleteContactAsync(contactId);

            if (!deleted)
            {
                ModelState.AddModelError("Contact", "It was not possible to delete the contact");
                return ValidationProblem();
            }

            return Ok();
        }

        [HttpGet(ApiRoutes.Contact.GetAll)]
        public async Task<ActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            List<ContactDTO> contactsDto = _mapper.Map<List<ContactDTO>>(contacts);
            return Ok(contactsDto);
        }
    }
}