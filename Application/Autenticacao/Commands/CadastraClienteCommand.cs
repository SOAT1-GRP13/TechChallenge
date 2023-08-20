using Application.Autenticacao.Boundaries.Cliente;
using Application.Autenticacao.Commands.Validation;
using Domain.Base.Messages;

namespace Application.Autenticacao.Commands
{
    public class CadastraClienteCommand : Command<AutenticaClienteOutput>
    {
        public CadastraClienteCommand(CadastraClienteInput input) 
        {
            Input = input;
        }

        public CadastraClienteInput Input { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new CadastraClienteValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
