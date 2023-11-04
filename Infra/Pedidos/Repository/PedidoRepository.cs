using Domain.Base.Data;
using Domain.Configuration;
using Domain.Pedidos;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Pedidos.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly PedidosContext _context;
        private readonly Secrets _settings;
        private readonly DbContextOptions<PedidosContext> _optionsBuilder;

        public PedidoRepository(PedidosContext context, IOptions<Secrets> options)
        {
            _context = context;
            _settings = options.Value;
            _optionsBuilder = new DbContextOptions<PedidosContext>();
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Pedido> ObterPorId(Guid id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return null;

            await _context.Entry(pedido)
                .Collection(i => i.PedidoItems).LoadAsync(); // Popula pedido evitando querys com join

            return pedido;
        }

        public async Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId)
        {
            return await _context.Pedidos.AsNoTracking().Where(p => p.ClienteId == clienteId).ToListAsync();
        }

        public async Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId)
        {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.ClienteId == clienteId && p.PedidoStatus == PedidoStatus.Rascunho);
            if (pedido == null) return null;

            await _context.Entry(pedido)
                .Collection(i => i.PedidoItems).LoadAsync(); // Popula pedido evitando querys com join

            return pedido;
        }

        public void Adicionar(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
        }

        public void Atualizar(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
        }


        public async Task<PedidoItem> ObterItemPorId(Guid id)
        {
            return await _context.PedidoItems.FindAsync(id);
        }

        public async Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId)
        {
            return await _context.PedidoItems.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.PedidoId == pedidoId);
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            _context.PedidoItems.Add(pedidoItem);
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            _context.PedidoItems.Update(pedidoItem);
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            _context.PedidoItems.Remove(pedidoItem);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<Pedido>> ObterTodosPedidos()
        {
            return await _context.Pedidos.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> ObterPedidosParaFila()
        {
            var pedido = await _context.Pedidos
                                       .Where(p => p.PedidoStatus != PedidoStatus.Finalizado
                                                && p.PedidoStatus != PedidoStatus.Rascunho
                                                && p.PedidoStatus != PedidoStatus.Cancelado)
                                       .Include(p => p.PedidoItems)
                                       .OrderBy(p => p.DataCadastro)
                                       .OrderBy(p => p.PedidoStatus)
                                       .ToListAsync();

            return pedido;
        }
    }
}
