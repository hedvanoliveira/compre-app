using System;

namespace CompreApp.Application.Features.PedidoFeature.ConsultarPedidos;

public record ConsultarPedidosResponse(Guid IdPedido, string NomeAplicativo, decimal Preco, DateTime DataPedido, string Situacao);

