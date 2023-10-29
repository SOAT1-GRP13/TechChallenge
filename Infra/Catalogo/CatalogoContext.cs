using Domain.Base.Data;
using Domain.Base.Messages;
using Domain.Catalogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infra.Catalogo
{
    public class CatalogoContext : DbContext, IUnitOfWork
    {
        protected readonly IConfiguration _configuration;
        public CatalogoContext(DbContextOptions<CatalogoContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Produto> Produtos => Set<Produto>();
        public DbSet<Categoria> Categorias => Set<Categoria>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetSection("ConnectionString").Value);
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

            modelBuilder.Entity<Categoria>()
                .HasData(
                    new Categoria("Lanche", 1, Guid.Parse("45dd547f-5209-49c6-8cd1-ee5c384e93f2")),
                    new Categoria("Acompanhamento", 2, Guid.Parse("9449c867-2f9e-4a3c-8e5f-06d18b788e88")),
                    new Categoria("Sobremesa", 3, Guid.Parse("8293ecf7-3cab-4a63-99e9-d23d7ea5cd67")),
                    new Categoria("Bebida", 4, Guid.Parse("90c75224-78fa-4ef1-8b0c-c328781709a4"))
                );

            modelBuilder.Entity<Produto>()
                .HasData(
                    new Produto("Cheeseburger", "Hamburguer de carne com queijo, alface e tomate", true, 28, Guid.Parse("45dd547f-5209-49c6-8cd1-ee5c384e93f2"), DateTime.UtcNow, "cheeseburger.jpg"),
                    new Produto("Batata Frita", "Batatas cortadas em palitos e fritas até ficarem crocantes.", true, 8, Guid.Parse("9449c867-2f9e-4a3c-8e5f-06d18b788e88"), DateTime.UtcNow, "batata_frita.jpg"),
                    new Produto("Coca-Cola - Lata", "Bebida gaseificada e refrescante", true, 5, Guid.Parse("90c75224-78fa-4ef1-8b0c-c328781709a4"), DateTime.UtcNow, "refrigerante.jpg"),
                    new Produto("Sundae de Chocolate", "Sorvete cremoso de baunilha coberto com calda de chocolate e pedaços de chocolate crocantes.", true, 12, Guid.Parse("8293ecf7-3cab-4a63-99e9-d23d7ea5cd67"), DateTime.UtcNow, "sundae_chocolate.jpg")
                );

            base.OnModelCreating(modelBuilder);
        }

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

            return await base.SaveChangesAsync() > 0;
        }
    }
}