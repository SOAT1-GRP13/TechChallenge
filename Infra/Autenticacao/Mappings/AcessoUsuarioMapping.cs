using Domain.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Autenticacao.Mappings
{
    public class AcessoUsuarioMapping : IEntityTypeConfiguration<AcessoUsuario>
    {
        public void Configure(EntityTypeBuilder<AcessoUsuario> builder)
        {
            builder.ToTable("acesso_usuario");

            builder.Property(x => x.NomeUsuario).HasColumnName("nome_usuario");
            builder.Property(x => x.Senha).HasColumnName("senha");
            builder.Property(x => x.Role).HasColumnName("role_usuario");
        }
    }
}
