using Application.Autenticacao.Boundaries.Cliente;
using Application.Autenticacao.Commands.Validation;
using Domain.Base.Messages;

namespace Application.Autenticacao.Commands
{
    public class AutenticaClienteCommand : Command<AutenticaClienteOutput>
    { 
        public AutenticaClienteCommand(AutenticaClienteInput input) 
        {
            Input = input;
        }

        public AutenticaClienteInput Input { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AutenticaClienteValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
