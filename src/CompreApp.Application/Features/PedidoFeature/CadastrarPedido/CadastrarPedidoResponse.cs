using System;

namespace CompreApp.Application.Features.PedidoFeature.CadastrarPedido;

public record CadastrarPedidoResponse(DateTime Data, string SituacaoPedido, CadastrarPedidoDetalheResponse? Detalhes);
public record CadastrarPedidoDetalheResponse(Guid CodigoPedido, decimal ValorPedido, string NomeProduto);