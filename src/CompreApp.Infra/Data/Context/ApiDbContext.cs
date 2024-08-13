using CompreApp.Domain.Entities;
using CompreApp.Infra.Data.Configurations;
using CompreApp.Infra.Data.SeedData;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CompreApp.Infra.Data.Context;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(message => Debug.WriteLine(message)).EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteEnderecoConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteCartaoConfiguration());
        modelBuilder.ApplyConfiguration(new PedidoConfiguration());
        modelBuilder.ApplyConfiguration(new PedidoPagamentoConfiguration());

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<ClienteEndereco> ClienteEnderecos { get; set; }
    public DbSet<ClienteCartao> ClienteCartoes { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoPagamento> PedidoPagamentos { get; set; }

}
