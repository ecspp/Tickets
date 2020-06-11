using FluentValidation;

namespace Tickets.WebAPI.Contracts.v1.Requests.Creation.Validation
{
    public class AddContactDtoValidator : AbstractValidator<AddContactDTO>
    {
        public AddContactDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("O tamanho máximo do nome é 100 caracteres");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email inválido");
        }
    }
}