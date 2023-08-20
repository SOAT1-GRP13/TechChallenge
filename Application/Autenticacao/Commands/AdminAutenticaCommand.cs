using Application.Autenticacao.Boundaries.LogIn;
using Application.Autenticacao.Commands.Validation;
using Domain.Base.Messages;

namespace Application.Autenticacao.Commands
{
    public class AdminAutenticaCommand : Command<LogInUsuarioOutput>
    {

        public AdminAutenticaCommand(LogInUsuarioInput input)
        {
            Input = input;
        }

        public LogInUsuarioInput Input { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AdminAutenticaValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
