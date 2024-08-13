using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CompreApp.Domain.Entities;

namespace CompreApp.Infra.Data.Configurations;

public class PedidoPagamentoConfiguration : IEntityTypeConfiguration<PedidoPagamento>
{
    public void Configure(EntityTypeBuilder<PedidoPagamento> builder)
    {
        builder.ToTable("PedidoPagamento");

        builder.Property(p => p.DataPagamento);

        builder.HasOne(x => x.Pedido)
            .WithMany(x => x.PedidoPagamentos)
            .HasForeignKey(x => x.PedidoId);
    }
}