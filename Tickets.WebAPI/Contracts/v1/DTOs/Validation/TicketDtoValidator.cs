using FluentValidation;

namespace Tickets.WebAPI.Contracts.v1.DTOs.Validation
{
    public class TicketDtoValidator : AbstractValidator<TicketDTO>
    {
        public TicketDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("O titulo é obrigatório");
            RuleFor(x => x.Title).MaximumLength(150).WithMessage("O tamanho máximo do título é 150 caracteres");
            RuleFor(x => x.Description).NotEmpty().WithMessage("A descrição é obrigatória");
            RuleFor(x => x.Description).MaximumLength(5000).WithMessage("O tamanho máximo da descrição é 5000 caracteres");
        }
    }
}