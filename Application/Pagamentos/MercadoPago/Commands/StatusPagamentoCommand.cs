using Application.Pagamentos.MercadoPago.Boundaries;
using Domain.Base.Messages;

namespace Application.Pagamentos.Commands
{
    public class StatusPagamentoCommand : Command<bool>
    {
        public StatusPagamentoCommand(WebHookInput input)
        {
Input = input;
        }

        public WebHookInput Input {get;set;}

        public override bool EhValido()
        {
            ValidationResult = new StatusPagamentoValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}