using Domain.Autenticacao;
using Domain.Autenticacao.Enums;
using Domain.Base.Data;
using Domain.Base.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infra.Autenticacao
{
    public class AutenticacaoContext : DbContext, IUnitOfWork
    {
        protected readonly IConfiguration _configuration;
        public AutenticacaoContext(DbContextOptions<AutenticacaoContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<AcessoUsuario> AcessoUsuario => Set<AcessoUsuario>();
        public DbSet<AcessoCliente> AcessoCliente => Set<AcessoCliente>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetSection("DatabaseSettings:ConnectionString").Value);
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

            //Adicionando usuario padrão
            //Senha Teste@123
            modelBuilder.Entity<AcessoUsuario>()
                .HasData(
                new AcessoUsuario(
                    Guid.NewGuid(),
                    "fiapUser",
                    "B5D3A85785B854548A440F1EA52F19EF920AB9ED29136B977861B5E36983DEA2C92F29D5939A427AAAEDBC67C3B48A6CD9E1D45483D3796E0A60F113240BB49C",
                    Roles.Gestor
                    )
        );

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
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