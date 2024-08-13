using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CompreApp.Domain.Entities;

namespace CompreApp.Infra.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Cliente");

        builder.Property(p => p.Nome)
            .IsRequired()       
            .HasMaxLength(100); 

        builder.Property(p => p.Cpf)
            .IsRequired()
            .HasMaxLength(11); //00000000000

        builder.Property(p => p.DataNascimento)
            .IsRequired();

        builder.Property(p => p.Sexo) //F,M
            .IsRequired()
            .HasMaxLength(1);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(p => p.Senha) //Criptografado
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(p => p.Pedidos)
            .WithOne(p => p.Cliente)
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}