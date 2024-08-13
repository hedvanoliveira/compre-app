using CompreApp.Application.Features.ProdutoFeature.ConsultarProdutos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace CompreApp.Api.Endpoints;

public static class ProdutoEndpoints
{
    public static void MapProdutoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api").WithTags("Aplicativos");
        
        group.MapGet("/aplicativos", [AllowAnonymous] async (IConsultarProdutoService service, IDistributedCache cache, CancellationToken token) =>
        {
            var produtosCache = await cache.GetAsync("produtos", token);

            if (produtosCache is null)
            {
                var result = await service.Consultar(token);

                await cache.SetAsync("produtos", Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result)), new()
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(60)
                }, token);

                return result;
            }

            return JsonSerializer.Deserialize<List<ConsultarProdutoResponse>>(produtosCache);
        })
        .WithOpenApi()
        .AllowAnonymous()
        .Produces<List<ConsultarProdutoResponse>>(StatusCodes.Status200OK)
        .WithDescription(description: "Listar aplicativos disponíveis para compra");

    }
}