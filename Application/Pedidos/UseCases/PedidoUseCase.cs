using AutoMapper;
using Domain.Pedidos;
using Domain.Base.DomainObjects;
using Application.Pedidos.Events;
using Application.Pedidos.Queries.DTO;
using Domain.Base.Messages.CommonMessages.Notifications;
using Domain.Base.Messages;
using System.Runtime.CompilerServices;
using Domain.Base.DomainObjects.DTO;
using Domain.Base.Messages.CommonMessages.IntegrationEvents.Pedidos;

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
            var pedidoItem = new PedidoItem(produtoId, nome, quantidade, valorUnitario);
            var pedido = await BuscarOuIniciarPedidoAsync(clienteId, produtoId, pedidoItem);

            var pedidoItemExistente = pedido.PedidoItemExistente(pedidoItem);
            pedido.AdicionarItem(pedidoItem);

            if (pedidoItemExistente)
                _pedidoRepository.AtualizarItem(pedidoItem);
            else
                _pedidoRepository.AdicionarItem(pedidoItem);

            _pedidoRepository.Atualizar(pedido);

            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(pedido.ClienteId, pedido.Id, produtoId, nome, valorUnitario, quantidade));
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
            pedido.AdicionarEvento(new PedidoProdutoAtualizadoEvent(clienteId, pedido.Id, produtoId, quantidade));

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

            if(pedidoItem is null)
                throw new DomainException("Item do pedido não encontrado na base!");

            if (!pedido.PedidoItemExistente(pedidoItem))
                throw new DomainException("Item do pedido não encontrado!");

            pedido.RemoverItem(pedidoItem);
            pedido.AdicionarEvento(new PedidoProdutoRemovidoEvent(clienteId, pedido.Id, produtoId));

            _pedidoRepository.RemoverItem(pedidoItem);
            _pedidoRepository.Atualizar(pedido);

            return await _pedidoRepository.UnitOfWork.Commit();
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

        public async Task<bool> IniciarPedido(Guid clienteId, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(clienteId);
            if (pedido is null)
                throw new DomainException("Pedido não encontrado!");

            pedido.IniciarPedido();

            var itensList = new List<Item>();
            pedido.PedidoItems.ToList().ForEach(i => itensList.Add(new Item { Id = i.ProdutoId, Quantidade = i.Quantidade }));
            var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = itensList };

            //Evento que dispara a retirada de itens do estoque no catalogo em ProdutoEventHandler.cs
            pedido.AdicionarEvento(new PedidoIniciadoEvent(pedido.Id, pedido.ClienteId, listaProdutosPedido, pedido.ValorTotal, nomeCartao, numeroCartao, expiracaoCartao, cvvCartao));

            _pedidoRepository.Atualizar(pedido);
            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> FinalizarPedido(Guid pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPorId(pedidoId);

            if (pedido is null)
                throw new DomainException("Pedido não encontrado!");

            pedido.FinalizarPedido();

            pedido.AdicionarEvento(new PedidoFinalizadoEvent(pedidoId));
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

        public async Task<bool> CancelarProcessamentoEEstornarEstoque(Guid pedidoId)
        {
            var pedido = await _pedidoRepository.ObterPorId(pedidoId);

            if (pedido is null)
                throw new DomainException("Pedido não encontrado!");

            var itensList = new List<Item>();
            pedido.PedidoItems.ToList().ForEach(i => itensList.Add(new Item { Id = i.ProdutoId, Quantidade = i.Quantidade }));
            var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = itensList };

            pedido.AdicionarEvento(new PedidoProcessamentoCanceladoEvent(pedido.Id, pedido.ClienteId, listaProdutosPedido));
            pedido.TornarRascunho();

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        #endregion

        #region Metodos privados
        private async Task<Pedido> BuscarOuIniciarPedidoAsync(Guid clienteId, Guid produtoId, PedidoItem pedidoItem)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(clienteId);

            if (pedido is not null)
               return pedido;

            pedido = Pedido.PedidoFactory.NovoPedidoRascunho(clienteId);
            pedido.AdicionarItem(pedidoItem);

            _pedidoRepository.Adicionar(pedido);
            pedido.AdicionarEvento(new PedidoRascunhoIniciadoEvent(clienteId, produtoId));

            return pedido;
        }
        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _pedidoRepository.Dispose();
        }
    }
}
