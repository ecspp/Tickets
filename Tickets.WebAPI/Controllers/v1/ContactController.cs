using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public ContactController(IContactService contactService)
        {
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
                return BadRequest(new ContactCreateResponse
                {
                    Succes = false
                });
            }

            return Ok(new ContactCreateResponse
            {
                Succes = true,
                Id = newContact.Id.ToString(),
                Name = newContact.Name
            });
        }

        // [HttpPut(ApiRoutes.Contact.Update)]
        // public async Task<ActionResult> Update([FromRoute] Guid contactId, [FromBody] ContactUpdateRequest updateRequest)
        // {

        // }
    }
}