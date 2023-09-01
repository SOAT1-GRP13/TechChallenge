using Domain.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Autenticacao.Mappings
{
    public class AcessoClienteMapping : IEntityTypeConfiguration<AcessoCliente>
    {
        public void Configure(EntityTypeBuilder<AcessoCliente> builder)
        {
            builder.ToTable("acesso_cliente");

            builder.Property(x => x.CPF).HasColumnName("cpf");
            builder.Property(x => x.Senha).HasColumnName("senha");
            builder.Property(x => x.Nome).HasColumnName("nome");
            builder.Property(x => x.Email).HasColumnName("email");
        }
    }
}