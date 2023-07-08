using Domain.Pedidos;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Domain.Base.Data;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages;
using Domain.Catalogo;
using System.Reflection;

namespace Infra.Pedidos
{
    public class PedidosContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;


        public PedidosContext(DbContextOptions<PedidosContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Pedido> Pedidos => Set<Pedido>();

        public DbSet<PedidoItem> PedidoItems => Set<PedidoItem>();


        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublicarEventos(this); //Se houver sucesso, publica os eventos

            return sucesso;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Ignore<Event>();

            var types = modelBuilder.Model.GetEntityTypes().Select(t => t.ClrType).ToHashSet();

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly(),
                t => t.GetInterfaces()
                .Any(i => i.IsGenericType
                && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
                && types.Contains(i.GenericTypeArguments[0]))
                );

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
         
            base.OnModelCreating(modelBuilder);
        }
    }
}
