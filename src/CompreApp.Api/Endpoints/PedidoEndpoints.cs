using CompreApp.Application.Common;
using CompreApp.Application.Features.PedidoFeature.CadastrarPedido;
using CompreApp.Application.Features.PedidoFeature.ConsultarPedidos;
using CompreApp.Application.Features.ProdutoFeature.ConsultarProdutos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace CompreApp.Api.Endpoints;

public static class PedidoEndpoints
{
    public static void MapPedidoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api").WithTags("Pedidos");

        group.MapPost("/pedidos/cadastrar", [Authorize] async (ICadastrarPedidoService service, ClaimsPrincipal user, CadastrarPedidoRequest request, CancellationToken token) =>
        {
            var idCliente = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await service.CadastrarPedido(request, idCliente, token);

            if (result.Detalhes != null)
                return Results.Created("", result);

            return Results.BadRequest(result);
        })
        .WithOpenApi()
        .Produces<CadastrarPedidoResponse>(StatusCodes.Status201Created)
        .Produces<CadastrarPedidoResponse>(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .WithRequestValidation<CadastrarPedidoRequest>()
         .WithDescription(description: "Efetuar compra de aplicativo com cliente autenticado. Será necessário informar o ID do produto e os dados de cartão de crédito");

        group.MapGet("/pedidos", [Authorize] async (IConsultarPedidosService service, ClaimsPrincipal user, CancellationToken token) =>
        {
            var idCliente = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));

            return await service.Consultar(idCliente, token);
        })
        .WithOpenApi()
        .Produces<List<ConsultarPedidosResponse>>(StatusCodes.Status200OK)
        .WithDescription("Consultar pedidos do cliente autenticado");
    }
}