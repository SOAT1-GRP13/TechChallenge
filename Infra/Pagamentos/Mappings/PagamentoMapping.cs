using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Pagamentos;

namespace Infra.Pagamentos.Mappings
{
    public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeCartao)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.NumeroCartao)
                .IsRequired()
                .HasColumnType("varchar(16)");

            builder.Property(c => c.ExpiracaoCartao)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.CvvCartao)
                .IsRequired()
                .HasColumnType("varchar(4)");

            // 1 : 1 => Pagamento : Transacao
            builder.HasOne(c => c.Transacao)
                .WithOne(c => c.Pagamento);

            builder.ToTable("Pagamentos");
        }
    }
}
