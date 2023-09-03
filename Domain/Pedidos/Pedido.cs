using Domain.Base.DomainObjects;

namespace Domain.Pedidos
{
    public class Pedido : Entity, IAggregateRoot
    {
        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        public Pedido(Guid clienteId, bool cupomUtilizado, decimal desconto, decimal valorTotal)
        {
            ClienteId = clienteId;
            ValorTotal = valorTotal;
            _pedidoItems = new List<PedidoItem>();
        }

        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }


        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(p => p.CalcularValor());
        }


        public bool PedidoItemExistente(PedidoItem item)
        {
            return _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);
        }

        public void AdicionarItem(PedidoItem item)
        {
            if (!item.EhValido()) return;

            item.AssociarPedido(Id);

            if (PedidoItemExistente(item))
            {
                var itemExistente = _pedidoItems.First(p => p.ProdutoId == item.ProdutoId);
                itemExistente.AdicionarUnidades(item.Quantidade);
                item = itemExistente;

                _pedidoItems.Remove(itemExistente);
            }

            item.CalcularValor();
            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem item)
        {
            if (!item.EhValido()) return;

            var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

            if (itemExistente == null) throw new DomainException("O item não pertence ao pedido");
            _pedidoItems.Remove(itemExistente);

            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem item)
        {
            if (!item.EhValido()) return;
            item.AssociarPedido(Id);

            var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

            if (itemExistente == null) throw new DomainException("O item não pertence ao pedido");

            _pedidoItems.Remove(itemExistente);
            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        public void AtualizarUnidades(PedidoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }

        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public void IniciarPedido()
        {
            PedidoStatus = PedidoStatus.Iniciado;
        }

        public void ColocarPedidoComoPago()
        {
            PedidoStatus = PedidoStatus.Pago;
        }

        public void CancelarPedido()
        {
            PedidoStatus = PedidoStatus.Cancelado;
        }

        public void ColocarPedidoComoPronto()
        {
            PedidoStatus = PedidoStatus.Pronto;
        }

        public void ColocarPedidoEmPreparacao()
        {
            PedidoStatus = PedidoStatus.EmPreparacao;
        }

        public void ColocarPedidoComoRecebido()
        {
            PedidoStatus = PedidoStatus.Recebido;
        }

        public void FinalizarPedido()
        {
            PedidoStatus = PedidoStatus.Finalizado;
        }

        public void AtualizarStatus(PedidoStatus status)
        {
            switch (status)
            {
                case PedidoStatus.Rascunho:
                    TornarRascunho();
                    break;
                case PedidoStatus.Iniciado:
                    IniciarPedido();
                    break;
                case PedidoStatus.Pago:
                    ColocarPedidoComoPago();
                    break;
                case PedidoStatus.Cancelado:
                    CancelarPedido();
                    break;
                case PedidoStatus.Pronto:
                    ColocarPedidoComoPronto();
                    break;
                case PedidoStatus.EmPreparacao:
                    ColocarPedidoEmPreparacao();
                    break;
                case PedidoStatus.Recebido:
                    ColocarPedidoComoRecebido();
                    break;
                case PedidoStatus.Finalizado:
                    FinalizarPedido();
                    break;
                default:
                    throw new DomainException("Status do pedido inválido");
            }
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId, // TODO - Caso o cliente não se identifique usar algum código de cliente anônimo.
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }
    }
}
