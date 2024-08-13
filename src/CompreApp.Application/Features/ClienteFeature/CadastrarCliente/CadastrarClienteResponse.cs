using System;

namespace CompreApp.Application.Features.ClienteFeature.CadastrarCliente;

public record CadastrarClienteResponse(DateTime Data, string Situacao, CadastrarClienteResponseDetalheResponse? Detalhes);
public record CadastrarClienteResponseDetalheResponse(Guid Id, string Nome, string Email);