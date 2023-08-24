using FluentValidation;
using Application.Pedidos.Boundaries;

namespace Application.Pedidos.Commands.Validation
{
    public class AtualizarStatusPedidoValidation : AbstractValidator<AtualizarStatusPedidoInput>
    {
        public AtualizarStatusPedidoValidation()
        {
            RuleFor(a => a.IdPedido)
                .NotEmpty()
                .WithMessage("Id do pedido é obrigatório");

            RuleFor(a => a.Status)
                .NotEmpty()
                .WithMessage("Status do pedido é obrigatório");
        }
    }
}
