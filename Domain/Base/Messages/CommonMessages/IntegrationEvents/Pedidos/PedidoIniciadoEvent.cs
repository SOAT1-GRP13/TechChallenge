﻿using Domain.Base.DomainObjects.DTO;


namespace Domain.Base.Messages.CommonMessages.IntegrationEvents.Pedidos
{
    public class PedidoIniciadoEvent : IntegrationEvent
    {
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal Total { get; private set; }
        public ListaProdutosPedido ProdutosPedido { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }

        public PedidoIniciadoEvent(Guid pedidoId, Guid clienteId, ListaProdutosPedido itens, decimal total, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            ProdutosPedido = itens;
            Total = total;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }
    }
}
