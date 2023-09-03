using Application.Pagamentos.MercadoPago.Boundaries;
using Domain.Base.Messages;

namespace Application.Pagamentos.MercadoPago.Commands
{
    public class StatusPagamentoCommand : Command<bool>
    {
        public StatusPagamentoCommand(long id, string topic)
        {
            Id = id;
            Topic = topic;
        }

        public long Id { get; set; }
        public string Topic { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new StatusPagamentoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}