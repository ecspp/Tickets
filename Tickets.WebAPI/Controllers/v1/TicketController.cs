using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tickets.WebAPI.Contracts.v1;
using Tickets.WebAPI.Contracts.v1.Requests;
using Tickets.WebAPI.Contracts.v1.Responses;
using Tickets.WebAPI.Services;
using Tickets.Domain;
using System;
using Tickets.WebAPI.Data;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tickets.WebAPI.Contracts.v1.DTOs;

namespace Tickets.WebAPI.Controllers.v1
{
    [Authorize]
    [ApiController]
    public class TicketController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService, IMapper mapper)
        {
            this._mapper = mapper;
            this._ticketService = ticketService;
        }

        [HttpPost(ApiRoutes.Ticket.Create)]
        [ProducesResponseType(typeof(TicketDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] TicketCreateRequest createRequest)
        {
            var userId = GetUserId();
            var companyId = GetCompanyId();

            var ticket = new Ticket
            {
                Title = createRequest.Title,
                Description = createRequest.Description,
                UserId = userId,
                CompanyId = companyId
            };

            var createdTicket = await _ticketService.CreateTicketAsync(ticket);
            var ticketId = ticket.Id.ToString();

            if (!createdTicket)
            {
                ModelState.AddModelError("Ticket", "Unable to save new ticket");
                return ValidationProblem();
            }

            var ticketDto = _mapper.Map<TicketDTO>(ticket);

            return Ok(ticketDto);
        }

        [HttpPut(ApiRoutes.Ticket.Update)]
        [ProducesResponseType(typeof(TicketDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update([FromRoute] Guid ticketId, [FromBody] TicketUpdateRequest updateRequest)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(ticketId);
            if (ticket == null)
            {
                ModelState.AddModelError("Ticket", "Ticket not found");
                return ValidationProblem();
            }

            ticket.Description = updateRequest.Description;
            ticket.Title = updateRequest.Title;

            var updated = await _ticketService.UpdateTicketAsync(ticket);

            if (!updated)
            {
                ModelState.AddModelError("Ticket", "Could not update the ticket");
                return ValidationProblem();
            }

            var ticketDto = _mapper.Map<TicketDTO>(ticket);

            return Ok(ticketDto);
        }

        [HttpGet(ApiRoutes.Ticket.Get)]
        [ProducesResponseType(typeof(TicketDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get([FromRoute] Guid ticketId)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(ticketId);
            

            if (ticket == null)
            {
                return NotFound();
            }

            var ticketDto = _mapper.Map<TicketDTO>(ticket);
            return Ok(ticketDto);
        }

        [HttpDelete(ApiRoutes.Ticket.Delete)]
        [ProducesResponseType(typeof(TicketDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> Delete([FromRoute] Guid ticketId)
        {
            var ticket = await _ticketService.DeleteTicketAsync(ticketId);

            if (!ticket)
            {
                return NotFound();
            }
            
            var ticketDto = _mapper.Map<TicketDTO>(ticket);
            return Ok(ticketDto);
        }

        [HttpGet(ApiRoutes.Ticket.GetAll)]
        [ProducesResponseType(typeof(List<TicketDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            var ticketsDtoList = _mapper.Map<List<TicketDTO>>(tickets);
            return Ok(ticketsDtoList);
        }
    }
}