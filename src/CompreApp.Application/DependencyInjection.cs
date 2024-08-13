using CompreApp.Application.Features.ClienteFeature.AutenticarCliente;
using CompreApp.Application.Features.ClienteFeature.CadastrarCliente;
using CompreApp.Application.Features.PedidoFeature.CadastrarPedido;
using CompreApp.Application.Features.PedidoFeature.ConsultarPedidos;
using CompreApp.Application.Features.ProdutoFeature.ConsultarProdutos;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CompreApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IConsultarProdutoService, ConsultarProdutoService>();

        services.AddScoped<IAutenticarClienteService, AutenticarClienteService>();
        services.AddScoped<ICadastrarClienteService, CadastrarClienteService>();

        services.AddScoped<ICadastrarPedidoService, CadastrarPedidoService>();
        services.AddScoped<IConsultarPedidosService, ConsultarPedidosService>();

        services.AddScoped<IValidator<CadastrarClienteRequest>, CadastrarClienteRequestValidator>();
        services.AddScoped<IValidator<AutenticarClienteRequest>, AutenticarClienteRequestValidator>();
        services.AddScoped<IValidator<CadastrarPedidoRequest>, CadastrarPedidoRequestValidator>();

        return services;
    }
}
