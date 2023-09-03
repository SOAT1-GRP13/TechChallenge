using Application.Pedidos.Boundaries;
using Application.Pedidos.Commands.Validation;
using Domain.Base.Messages;

namespace Application.Pedidos.Commands
{
    public class ConsultarStatusPedidoCommand : Command<ConsultarStatusPedidoOutput>
    {
        public ConsultarStatusPedidoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new ConsultarStatusPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ConfirmarStatusPedidoOutput
    {
    }
}