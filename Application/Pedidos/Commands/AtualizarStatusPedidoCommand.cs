using Domain.Base.Messages;
using Application.Pedidos.Boundaries;
using Application.Pedidos.Commands.Validation;

namespace Application.Pedidos.Commands
{
    public class AtualizarStatusPedidoCommand : Command<PedidoOutput>
    {
        public AtualizarStatusPedidoInput Input { get; set; }

        public AtualizarStatusPedidoCommand(AtualizarStatusPedidoInput input)
        {
            Input = input;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarStatusPedidoValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
