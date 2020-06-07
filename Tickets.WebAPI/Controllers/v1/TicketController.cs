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

namespace Tickets.WebAPI.Controllers.v1
{
    [Authorize]
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly DataContext _dataContext;
        public TicketController(ITicketService ticketService, DataContext dataContext)
        {
            this._dataContext = dataContext;
            this._ticketService = ticketService;

        }

        [HttpPost(ApiRoutes.Ticket.Create)]
        public async Task<ActionResult> Create([FromBody] TicketCreateRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new TicketCreateResponse
                {
                    Success = false,
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
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
                return BadRequest(new TicketCreateResponse
                {
                    Success = false,
                    Errors = new[] { "Não foi possível salvar o ticket" }
                });
            }

            return Ok(new TicketCreateResponse
            {
                Success = true,
                Id = ticket.Id.ToString()
            });
        }

        [HttpPut(ApiRoutes.Ticket.Update)]
        public async Task<ActionResult> Update([FromRoute] Guid ticketId, [FromBody] TicketUpdateRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new TicketUpdateResponse
                {
                    Success = false,
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage))
                });
            }
            
            var ticket = await _ticketService.GetTicketByIdAsync(ticketId, GetCompanyId());
            if (ticket == null)
            {
                return BadRequest(new TicketUpdateResponse
                {
                    Success = false,
                    Errors = new[] { "Ticket not found" }
                });
            }

            ticket.Description = updateRequest.Description;
            ticket.Title = updateRequest.Title;

            var updated = await _ticketService.UpdateTicketAsync(ticket);

            if (!updated)
            {
                return BadRequest(new TicketUpdateResponse
                {
                    Success = false,
                    Errors = new[] { "Could not update the ticket" }
                });
            }

            return Ok(new TicketUpdateResponse
            {
                Success = true
            });
        }

        [HttpGet(ApiRoutes.Ticket.Get)]
        public async Task<ActionResult> Get([FromRoute] Guid ticketId)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(ticketId, GetCompanyId());

            if (ticket == null)
            {
                return BadRequest(new {Errors = new[] {"Ticket not found"}});
            }

            return Ok(new TicketDTO{
                Title = ticket.Title,
                Description = ticket.Description,
                UserId = ticket.UserId.ToString(),
                UserName = ticket.User.UserName
            });
        }

        [HttpGet(ApiRoutes.Ticket.Delete)]
        public async Task<ActionResult> Delete([FromRoute] Guid ticketId)
        {
            var ticket = await _ticketService.DeleteTicketAsync(ticketId, GetUserId());

            if (!ticket) {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet(ApiRoutes.Ticket.GetAll)]
        public async Task<ActionResult> GetAll()
        {
            var tickets = await _ticketService.GetAllTicketsAsync(GetUserId());

            return Ok(new {Tickets = tickets});
        }
    }
}