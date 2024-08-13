using CompreApp.Infra.Data.Context;
using CompreApp.Infra.Data.Repositories;
using CompreApp.Infra.Data.Repositories.Interfaces;
using CompreApp.Infra.Data.SeedData;
using CompreApp.Infra.GatewayPagamento.ApiPagamento;
using CompreApp.Infra.Pagamento.Integracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompreApp.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServerConnection");
        services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connectionString));

        services.AddTransient<MigracaoData>();

        services.AddScoped<IIntegracaoPagamentoService, ApiPagamentoMock>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        return services;
    }
}
