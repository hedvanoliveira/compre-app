using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CompreApp.Domain.Entities;

namespace CompreApp.Infra.Data.Configurations;

public class ClienteCartaoConfiguration : IEntityTypeConfiguration<ClienteCartao>
{
    public void Configure(EntityTypeBuilder<ClienteCartao> builder)
    {
        builder.ToTable("ClienteCartao");

        builder.Property(p => p.NomeProprietarioCartao)
            .IsRequired()       
            .HasMaxLength(100); 

        builder.Property(p => p.NumeroCartao)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(p => p.MesVencimento)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(p => p.AnoVencimento)
            .IsRequired()
            .HasMaxLength(4);

        builder.Property(p => p.CodigoSeguranca)
            .IsRequired()
            .HasMaxLength(4);

        builder.Property(p => p.BandeiraCartao)
           .IsRequired()
           .HasMaxLength(10);

        builder.HasOne(x => x.Cliente)
           .WithMany(x => x.ClienteCartoes)
           .HasForeignKey(x => x.ClienteId);
    }
}