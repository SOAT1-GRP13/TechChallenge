﻿using Domain.Base.Messages;
using FluentValidation;

namespace Application.Pedidos.Commands
{
    public class AdicionarItemPedidoCommand : Command
    {
        //Um comando representa uma intenção de mudança do estado da entidade no banco e na aplicação.
        public Guid ClienteId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public AdicionarItemPedidoCommand(Guid clienteId, Guid produtoId, string nome, int quantidade, decimal valorUnitario)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarItemPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }    
    }

    public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public static string IdClienteErroMsg => "Id do cliente inválido";
        public static string IdProdutoErroMsg => "Id do produto inválido";
        public static string NomeErroMsg => "O nome do produto não foi informado";
        public static string QtdMaxErroMsg => "A quantidade máxima de um item é 10";
        public static string QtdMinErroMsg => "A quantidade miníma de um item é 1";
        public static string ValorErroMsg => "O valor do item precisa ser maior que 0";

        public AdicionarItemPedidoValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdClienteErroMsg);

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdProdutoErroMsg);

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(NomeErroMsg);

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage(QtdMinErroMsg);

            RuleFor(c => c.Quantidade)
                .LessThan(10)
                .WithMessage(QtdMaxErroMsg);

            RuleFor(c => c.ValorUnitario)
                .GreaterThan(0)
                .WithMessage(ValorErroMsg);
        }
    }
}
