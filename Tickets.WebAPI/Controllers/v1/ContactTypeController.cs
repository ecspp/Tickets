using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class ContactTypeController : ControllerBase
    {
        private readonly IContactTypeService _contactTypeService;
        private readonly IMapper _mapper;
        public ContactTypeController(IContactTypeService contactTypeService, IMapper mapper)
        {
            this._mapper = mapper;
            this._contactTypeService = contactTypeService;
        }

        [HttpPost(ApiRoutes.ContactType.Create)]
        public async Task<ActionResult> Create([FromBody] AddContactTypeDTO createRequest)
        {
            var newContactType = new ContactType
            {
                Name = createRequest.Name,
            };
            var created = await _contactTypeService.CreateAsync(newContactType);
            if (!created)
            {
                ModelState.AddModelError("ContactType", "Não foi possível crir o tipo");
                return ValidationProblem();
            }

            var contactTypeDto = new ContactTypeDTO
            {
                Id = newContactType.Id,
                Name = newContactType.Name
            };

            return Ok(contactTypeDto);
        }

        [HttpPut(ApiRoutes.ContactType.Update)]
        public async Task<ActionResult> Update([FromBody] ContactTypeDTO updateRequest)
        {
            var contactType = await _contactTypeService.GetByIdAsync(updateRequest.Id);
            if (contactType == null)
            {
                ModelState.AddModelError("ContactType", "Tipo não encontrado");
                return ValidationProblem();
            }

            contactType.Name = updateRequest.Name;

            var updated = await _contactTypeService.UpdateAsync(contactType);
            if (!updated)
            {
                ModelState.AddModelError("Contact-Type", "Ocorreu um erro ao atualizar o tipo");
                return ValidationProblem();
            }

            var contactTypeDto = new ContactTypeDTO
            {
                Id = contactType.Id,
                Name = contactType.Name
            };
            return Ok(contactTypeDto);
        }

        [HttpGet(ApiRoutes.ContactType.Get)]
        public async Task<ActionResult> Get([FromRoute] int id)
        {
            var contactType = await _contactTypeService.GetByIdAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }

            var contactTypeDto = new ContactTypeDTO
            {
                Id = contactType.Id,
                Name = contactType.Name
            };
            return Ok(contactTypeDto);
        }

        [HttpDelete(ApiRoutes.ContactType.Delete)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _contactTypeService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet(ApiRoutes.ContactType.GetAll)]
        public async Task<ActionResult> GetAll()
        {
            var types = await _contactTypeService.GetAllAsync();
            List<ContactTypeDTO> typesDto = _mapper.Map<List<ContactTypeDTO>>(types);
            return Ok(typesDto);
        }

    }
}