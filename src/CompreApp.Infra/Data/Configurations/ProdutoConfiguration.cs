using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CompreApp.Domain.Entities;

namespace CompreApp.Infra.Data.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produto");

        builder.Property(p => p.Nome)
            .IsRequired()       
            .HasMaxLength(100); 

        builder.Property(p => p.Descricao)
            .HasMaxLength(400);

        builder.Property(p => p.Preco)
            .IsRequired() 
            .HasColumnType("decimal(18,2)");
    }
}