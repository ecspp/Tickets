using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tickets.Domain;
using Tickets.WebAPI.Contracts.v1;
using Tickets.WebAPI.Contracts.v1.DTOs;
using Tickets.WebAPI.Contracts.v1.Requests.Creation;
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
        [ProducesResponseType(typeof(ContactDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddContactDTO createRequest)
        {
            Contact newContact = await _contactService.CreateAsync(createRequest);
            if (newContact == null)
            {
                ModelState.AddModelError("Contact", "It was not possible to create the contact");
                return ValidationProblem();
            }
            
            var contactDto = _mapper.Map<ContactDTO>(newContact);
            return Ok(contactDto);
        }

        [HttpPut(ApiRoutes.Contact.Update)]
        [ProducesResponseType(typeof(ContactDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update([FromRoute] int contactId, [FromBody] ContactDTO updateRequest)
        {
            var contact = await _contactService.GetByIdAsync(contactId);
            if (contact == null)
            {
                ModelState.AddModelError("Contact", "Contact not found");
                return ValidationProblem();
            }

            var updated = await _contactService.UpdateAsync(updateRequest, contact);
            if (!updated)
            {
                ModelState.AddModelError("Contact", "An error ocurred while updating the contact");
                return ValidationProblem();
            }

            var contactDto = _mapper.Map<ContactDTO>(await _contactService.GetJoinedById(contactId));

            return Ok(contactDto);
        }

        [HttpGet(ApiRoutes.Contact.Get)]
        [ProducesResponseType(typeof(ContactDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAction([FromRoute] int contactId)
        {
            var contact = await _contactService.GetByIdAsync(contactId);

            if (contact == null)
            {
                return NotFound();
            }

            var contactDto = _mapper.Map<ContactDTO>(contact);

            return Ok(contactDto);
        }

        [HttpDelete(ApiRoutes.Contact.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] int contactId)
        {
            var deleted = await _contactService.DeleteAsync(contactId);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet(ApiRoutes.Contact.GetAll)]
        [ProducesResponseType(typeof(List<ContactDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllAsync();
            List<ContactDTO> contactsDto = _mapper.Map<List<ContactDTO>>(contacts);
            return Ok(contactsDto);
        }
    }
}