using AutoMapper;
using Domain.Pedidos;
using Domain.Base.DomainObjects;
using Application.Pedidos.Queries.DTO;

namespace Application.Pedidos.UseCases
{
    public class PedidoUseCase : IPedidoUseCase
    {
        #region Propriedades
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Construtor
        public PedidoUseCase(
            IPedidoRepository pedidoRepository,
            IMapper mapper
        )
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }
        #endregion

        #region Use Cases
        public async Task<bool> AdicionarItem(Guid clienteId, Guid produtoId, string nome, int quantidade, decimal valorUnitario)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(clienteId);
            var pedidoItem = new PedidoItem(produtoId, nome, quantidade, valorUnitario);

            // Assim que cliente colocar um item no carrinho ele ja iniciou um pedido.
            if (pedido is null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(clienteId);
                pedido.AdicionarItem(pedidoItem);

                _pedidoRepository.Adicionar(pedido);
            }
            else
            {
                var pedidoItemExistente = pedido.PedidoItemExistente(pedidoItem);
                pedido.AdicionarItem(pedidoItem);

                if (pedidoItemExistente)
                {
                    _pedidoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
                }
                else
                {
                    _pedidoRepository.AdicionarItem(pedidoItem);
                }
            }

            return await _pedidoRepository.UnitOfWork.Commit();
        }
        public async Task<bool> AtualizarItem(Guid clienteId, Guid produtoId, int quantidade)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(clienteId);
            if (pedido is null)
                throw new DomainException("Pedido não encontrado!");
            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, produtoId);
            if (!pedido.PedidoItemExistente(pedidoItem))
                throw new DomainException("Item do pedido não encontrado!");
            pedido.AtualizarUnidades(pedidoItem, quantidade);
            _pedidoRepository.AtualizarItem(pedidoItem);
            _pedidoRepository.Atualizar(pedido);
            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> RemoverItem(Guid clienteId, Guid produtoId)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(clienteId);

            if (pedido is null)
                throw new DomainException("Pedido não encontrado!");

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, produtoId);

            if (pedidoItem is null)
                throw new DomainException("Item do pedido não encontrado na base!");

            if (!pedido.PedidoItemExistente(pedidoItem))
                throw new DomainException("Item do pedido não encontrado!");

            pedido.RemoverItem(pedidoItem);

            _pedidoRepository.RemoverItem(pedidoItem);
            _pedidoRepository.Atualizar(pedido);

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<PedidoDto> TrocaStatusPedidoWebhook(int mercadoPagoId, PedidoStatus novoStatus)
        {
            var pedido = await _pedidoRepository.ObterPorIdMercadoPago(mercadoPagoId);

            if (pedido is null)
                return new PedidoDto();

            pedido.AtualizarStatus(novoStatus);

            _pedidoRepository.Atualizar(pedido);

            await _pedidoRepository.UnitOfWork.Commit();

            return _mapper.Map<PedidoDto>(pedido);
        }

        public async Task<PedidoDto> TrocaStatusPedido(Guid idPedido, PedidoStatus novoStatus)
        {
            var pedido = await _pedidoRepository.ObterPorId(idPedido);

            if (pedido is null)
                return new PedidoDto();

            pedido.AtualizarStatus(novoStatus);

            _pedidoRepository.Atualizar(pedido);

            await _pedidoRepository.UnitOfWork.Commit();

            return _mapper.Map<PedidoDto>(pedido);
        }

        public async Task<bool> IniciarPedido(Guid pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPorId(pedidoId);
            if (pedido is null)
                throw new DomainException("Pedido não encontrado!");

            //TODO integrar com Mercado pago e adicionar um Id referente ao pagamento
            pedido.IniciarPedido();

            // var itensList = new List<Item>();
            // pedido.PedidoItems.ToList().ForEach(i => itensList.Add(new Item { Id = i.ProdutoId, Quantidade = i.Quantidade }));
            // var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = itensList };

            _pedidoRepository.Atualizar(pedido);
            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> FinalizarPedido(Guid pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPorId(pedidoId);

            if (pedido is null)
                throw new DomainException("Pedido não encontrado!");

            pedido.FinalizarPedido();
            _pedidoRepository.Atualizar(pedido);

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> CancelarProcessamento(Guid pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPorId(pedidoId);

            if (pedido is null)
                throw new DomainException("Pedido não encontrado!");

            pedido.TornarRascunho();

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<PedidoDto> ObterPedidoPorId(Guid pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPorId(pedidoId);

            return _mapper.Map<PedidoDto>(pedido);
        }

        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _pedidoRepository.Dispose();
        }


    }
}
