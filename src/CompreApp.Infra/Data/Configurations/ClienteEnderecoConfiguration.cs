using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CompreApp.Domain.Entities;

namespace CompreApp.Infra.Data.Configurations;

public class ClienteEnderecoConfiguration : IEntityTypeConfiguration<ClienteEndereco>
{
    public void Configure(EntityTypeBuilder<ClienteEndereco> builder)
    {
        builder.ToTable("ClienteEndereco");

        builder.Property(p => p.Logradouro)
            .HasMaxLength(100);

        builder.Property(p => p.Cep) //00000000
            .IsRequired()       
            .HasMaxLength(8); 

        builder.Property(p => p.Uf)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(p => p.Municipio)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Bairro)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Numero) //Opcional
            .HasMaxLength(10); 
        
        builder.Property(p => p.Complemento) //Opcional
            .HasMaxLength(100);

        builder.HasOne(x => x.Cliente)
           .WithMany(x => x.ClienteEnderecos)
           .HasForeignKey(x => x.ClienteId);
    }
}