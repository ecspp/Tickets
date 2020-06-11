using FluentValidation;

namespace Tickets.WebAPI.Contracts.v1.DTOs.Validation
{
    public class ContactDtoValidator : AbstractValidator<ContactDTO>
    {
        public ContactDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("O tamanho máximo do nome é 100 caracteres");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email inválido");
        }
    }
}