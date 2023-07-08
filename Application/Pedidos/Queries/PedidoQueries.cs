using Application.Pedidos.Queries.DTO;
using Domain.Pedidos;


namespace Application.Pedidos.Queries
{
    public class PedidoQueries : IPedidoQueries
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoQueries(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<CarrinhoDto> ObterCarrinhoCliente(Guid clienteId)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(clienteId);
            if (pedido == null) return null;

            var carrinho = new CarrinhoDto
            {
                ClienteId = pedido.ClienteId,
                ValorTotal = pedido.ValorTotal,
                PedidoId = pedido.Id,
                SubTotal = pedido.ValorTotal // No futuro, se houver desconto, o subTotal será diferente do valor total
            };



            foreach (var item in pedido.PedidoItems)
            {
                carrinho.Items.Add(new CarrinhoItemDto
                {
                    ProdutoId = item.ProdutoId,
                    ProdutoNome = item.ProdutoNome,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.ValorUnitario * item.Quantidade
                });
            }

            return carrinho;
        }

        public async Task<IEnumerable<PedidoDto>> ObterPedidosCliente(Guid clienteId)
        {
            var pedidos = await _pedidoRepository.ObterListaPorClienteId(clienteId);

            pedidos = pedidos.Where(p => p.PedidoStatus == PedidoStatus.Pago || p.PedidoStatus == PedidoStatus.Cancelado)
                .OrderByDescending(p => p.Codigo);

            if (!pedidos.Any()) return null;

            var pedidosView = new List<PedidoDto>();

            foreach (var pedido in pedidos)
            {
                pedidosView.Add(new PedidoDto
                {
                    Id = pedido.Id,
                    ValorTotal = pedido.ValorTotal,
                    PedidoStatus = pedido.PedidoStatus,
                    Codigo = pedido.Codigo,
                    DataCadastro = pedido.DataCadastro
                });
            }

            return pedidosView;
        }

        public async Task<IEnumerable<PedidoDto>> ObterTodosPedidos()
        {
            var pedidos = await _pedidoRepository.ObterTodosPedidos();

            if (!pedidos.Any()) return null;

            var pedidosView = new List<PedidoDto>();

            foreach (var pedido in pedidos)
            {
                pedidosView.Add(new PedidoDto
                {
                    Id = pedido.Id,
                    ValorTotal = pedido.ValorTotal,
                    PedidoStatus = pedido.PedidoStatus,
                    Codigo = pedido.Codigo,
                    DataCadastro = pedido.DataCadastro
                });
            }

            return pedidosView;
        }
    }
}
