using System;
using FluentValidation;
using Domain.Base.Messages;
using Application.Pedidos.Boundaries;

namespace Application.Pedidos.Commands
{
    public class IniciarPedidoCommand : Command<ConfirmarPedidoOutput>
    {
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }

        public IniciarPedidoCommand(Guid pedidoId, Guid clienteId)
        {
            PedidoId = pedidoId;
            ClienteId = clienteId;
        }

        public override bool EhValido()
        {
            ValidationResult = new IniciarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class IniciarPedidoValidation : AbstractValidator<IniciarPedidoCommand>
    {
        public IniciarPedidoValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do pedido inválido");
        }
    }
}