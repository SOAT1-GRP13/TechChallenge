using FluentValidation;

namespace Application.Pedidos.Commands.Validation
{
    public class ConsultarStatusPedidoValidation : AbstractValidator<ConsultarStatusPedidoCommand>
    {
        public ConsultarStatusPedidoValidation()
        {
            RuleFor(a => a.Id)
                .NotEmpty()
                .WithMessage("Id do pedido é obrigatório");
        }
    }
}
