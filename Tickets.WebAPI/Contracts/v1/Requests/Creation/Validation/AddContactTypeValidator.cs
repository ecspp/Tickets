using FluentValidation;

namespace Tickets.WebAPI.Contracts.v1.Requests.Creation.Validation
{
    public class AddContactTypeValidator : AbstractValidator<AddContactTypeDTO>
    {
        public AddContactTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Tamanho máximo do nome é 100 caracteres");
        }
    }
}