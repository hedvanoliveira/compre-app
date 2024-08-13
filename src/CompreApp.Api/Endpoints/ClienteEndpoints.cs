using CompreApp.Application.Common;
using CompreApp.Application.Features.ClienteFeature.AutenticarCliente;
using CompreApp.Application.Features.ClienteFeature.CadastrarCliente;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CompreApp.Api.Endpoints;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api").WithTags("Clientes");

        group.MapPost("/clientes/autenticar", [AllowAnonymous] async (IAutenticarClienteService service, AutenticarClienteRequest request, CancellationToken token) =>
        {
            var result = await service.Autenticar(request, token);

            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
 
        })
        .WithOpenApi()
        .Produces<AutenticarClienteResponse>(StatusCodes.Status200OK)
        .Produces<List<ValidationResult>>(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .WithRequestValidation<AutenticarClienteRequest>()
        .WithDescription("Autenticar cliente com e-mail e senha");

        group.MapPost("/clientes/cadastrar", [AllowAnonymous] async (ICadastrarClienteService service, CadastrarClienteRequest request, CancellationToken token) =>
        {
            var result = await service.CadastrarCliente(request, token);

            if (result.Detalhes != null)
                return Results.Created("", result);

            return Results.BadRequest(result);
        })
        .WithOpenApi()
        .Produces<CadastrarClienteResponse>(StatusCodes.Status201Created)
        .Produces<CadastrarClienteResponse>(StatusCodes.Status400BadRequest)
        .Produces<List<ValidationResult>>(StatusCodes.Status400BadRequest)
        .Produces<ErroGlobal>(StatusCodes.Status500InternalServerError)
        .WithRequestValidation<CadastrarClienteRequest>()
        .WithDescription("Cadastrar novo cliente com endereço");

        group.MapGet("/clientes/info", [Authorize] (ClaimsPrincipal user) =>
        {
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var nome = user.FindFirstValue(ClaimTypes.Name);
            var email = user.FindFirstValue(ClaimTypes.Email);
            return Results.Ok(new ClienteInfoResponse(Guid.Parse(id), nome, email));
        })
        .WithOpenApi()
        .Produces<ClienteInfoResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .WithDescription("Exibir imformações do cliente autenticado");
    }
}