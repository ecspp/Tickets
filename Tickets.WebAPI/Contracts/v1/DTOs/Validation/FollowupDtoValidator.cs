using System.Threading.Tasks;
using FluentValidation;
using Tickets.WebAPI.Services;

namespace Tickets.WebAPI.Contracts.v1.DTOs.Validation
{
    public class FollowupDtoValidator : AbstractValidator<FollowupDTO>
    {
        private readonly ITicketService _ticketService;
        public FollowupDtoValidator(ITicketService ticketService)
        {
            this._ticketService = ticketService;
            RuleFor(x => x.Title).NotEmpty().WithMessage("O titulo é obrigatório");
            RuleFor(x => x.Title).MaximumLength(150).WithMessage("O tamanho máximo do título é 150 caracteres");
            RuleFor(x => x.Description).NotEmpty().WithMessage("A descrição é obrigatória");
            RuleFor(x => x.Description).MaximumLength(5000).WithMessage("O tamanho máximo da descrição é 5000 caracteres");
            RuleFor(x => x.TicketId).MustAsync((x, cancellation) => TicketIdIsValid(x)).WithMessage("Ticket inválido");
        }

        private async Task<bool> TicketIdIsValid(long ticketId)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(ticketId);
            if (ticket != null)
            {
                return true;
            }
            return false;
        }
    }
}