using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tickets.Domain;
using Tickets.Domain.Identity;
using Tickets.Repository;
using Tickets.WebAPI.DTOs;

namespace Tickets.WebAPI.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public TicketController(ITicketsRepository ticketsRepository, IMapper mapper, UserManager<User> userManager)
        {
            this._ticketsRepository = ticketsRepository;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketDTO ticketDto)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var newTicket = _mapper.Map<Ticket>(ticketDto);
            newTicket.UserId = currentUser.Id;
            newTicket.CompanyId = currentUser.CompanyId;
            _ticketsRepository.Add<Ticket>(newTicket);
            if (await _ticketsRepository.SaveChangesAsync()) 
            {
                return Ok(newTicket);
            }
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível criar o novo ticket");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets() 
        {
            int companyId = (await GetLoggedInUser()).CompanyId;
            var result = await _ticketsRepository.GetAllTickets(companyId);
            var resultDtos = _mapper.Map<List<TicketDTO>>(result);
            return Ok(resultDtos);
        }

        private async Task<User> GetLoggedInUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return user;
        }

    }
}