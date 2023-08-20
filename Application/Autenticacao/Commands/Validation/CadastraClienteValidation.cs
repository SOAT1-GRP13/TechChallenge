using Application.Autenticacao.Boundaries.Cliente;
using FluentValidation;

namespace Application.Autenticacao.Commands.Validation
{
    public class CadastraClienteValidation : AbstractValidator<CadastraClienteInput>
    {
        public CadastraClienteValidation()
        {
            RuleFor(a => a.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório");

            RuleFor(a => a.Senha)
                .NotEmpty()
                .WithMessage("Senha é obrigatória");

            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("E-mail é obrigatório");

            RuleFor(a => a.Email)
                .EmailAddress()
                .WithMessage("E-mail inválido");

            RuleFor(a => a.CPF)
                .NotEmpty()
                .WithMessage("CPF é obrigatório");
        }
    }
}
