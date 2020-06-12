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
    public class FollowupController : BaseController
    {
        private readonly IFollowupService _followupService;
        private readonly IMapper _mapper;
        public FollowupController(IFollowupService followupService, IMapper mapper)
        {
            this._mapper = mapper;
            this._followupService = followupService;
        }

        [HttpPost(ApiRoutes.Followup.Create)]
        public async Task<ActionResult> Create([FromBody] AddFollowupDTO createRequest)
        {
            var newFollowup = new Followup 
            {
                Title = createRequest.Title,
                Description = createRequest.Description,
                TicketId = createRequest.TicketId
            };

            var created = await _followupService.CreateFollowupAsync(newFollowup);
            if (!created)
            {
                ModelState.AddModelError("Followup", "Não foi possível salvar o followup");
                return ValidationProblem();
            }

            var newFolloupDto = _mapper.Map<FollowupDTO>(newFollowup);
            return Ok(newFolloupDto);
        }

        [HttpGet(ApiRoutes.Followup.GetAllFromTicket)]
        public async Task<ActionResult> GetAllFromTicket([FromRoute] long ticketId)
        {
            var followups = await _followupService.GetAllFollowupsFromTicketAsync(ticketId);
            var followupsDto = _mapper.Map<List<FollowupDTO>>(followups);
            return Ok(followupsDto);
        }

        [HttpGet(ApiRoutes.Followup.Get)]
        public async Task<ActionResult> Get([FromRoute] long followupId)
        {
            var followup = await _followupService.GetFollowupByIdAsync(followupId);

            if (followup == null)
            {
                return NotFound();
            }

            var followupDto = _mapper.Map<FollowupDTO>(followup);
            return Ok(followupDto);
        }

        [HttpPut(ApiRoutes.Followup.Update)]
        public async Task<ActionResult> Update([FromBody] FollowupDTO updateRequest)
        {
            var followup = _mapper.Map<Followup>(updateRequest);
            var updated = await _followupService.UpdateFollowupAsync(followup);
            if (!updated)
            {
                ModelState.AddModelError("Followup", "Não foi possível salvar o followup");
                return ValidationProblem();
            }

            var updatedFollowupDto = _mapper.Map<FollowupDTO>(followup);
            return Ok(updatedFollowupDto);
        }

        [HttpDelete(ApiRoutes.Followup.Delete)]
        public async Task<ActionResult> Delete([FromRoute] long followupId)
        {
            var deleted = await _followupService.DeleteFollowupAsync(followupId);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}