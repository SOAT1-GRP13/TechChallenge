using Application.Autenticacao.Boundaries.Cliente;
using FluentValidation;

namespace Application.Autenticacao.Commands.Validation
{
    public class AutenticaClienteValidation : AbstractValidator<AutenticaClienteInput>
    {
        public AutenticaClienteValidation()
        {
            RuleFor(c => c.CPF)
                .NotEmpty()
                .WithMessage("CPF é obrigatório");

            RuleFor(s => s.Senha)
                .NotEmpty()
                .WithMessage("Senha é obrigatório");
        }
    }
}
