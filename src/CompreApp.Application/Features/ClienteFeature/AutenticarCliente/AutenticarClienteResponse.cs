using System;

namespace CompreApp.Application.Features.ClienteFeature.AutenticarCliente;

public record AutenticarClienteResponse(Guid Id, string Nome, string Email, string Token);

public record ClienteInfoResponse(Guid Id, string Nome, string Email);