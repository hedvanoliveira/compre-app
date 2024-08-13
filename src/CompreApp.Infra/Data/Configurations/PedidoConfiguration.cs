using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CompreApp.Domain.Entities;

namespace CompreApp.Infra.Data.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedido");

        builder.Property(p => p.DataFinalizacaoPedido);

        builder.Property(p => p.Preco)
           .IsRequired()
           .HasColumnType("decimal(18,2)");

        builder.HasOne(p => p.Cliente)
            .WithMany(p => p.Pedidos)
            .HasForeignKey(p => p.ClienteId);

        builder.HasMany(p => p.PedidoPagamentos)
              .WithOne(pag => pag.Pedido)
              .HasForeignKey(pag => pag.PedidoId)
              .OnDelete(DeleteBehavior.NoAction);
    }
}