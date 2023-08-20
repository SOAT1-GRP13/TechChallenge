using Application.Autenticacao.Boundaries.LogIn;
using FluentValidation;

namespace Application.Autenticacao.Commands.Validation
{
    public class AdminAutenticaValidation : AbstractValidator<LogInUsuarioInput>
    {
        public AdminAutenticaValidation() 
        {
            RuleFor(a => a.NomeUsuario)
                .NotEmpty()
                .WithMessage("Nome do usuário é obrigatório");

            RuleFor(s => s.Senha)
                .NotEmpty()
                .WithMessage("Senha é obrigatório");
        }
    }
}
