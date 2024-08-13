using CompreApp.Application.Common;
using CompreApp.Domain.Entities;
using CompreApp.Infra.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompreApp.Application.Features.PedidoFeature.ConsultarPedidos;

public class ConsultarPedidosService(IPedidoRepository pedidoRepository) : IConsultarPedidosService
{
    private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

    public async Task<List<ConsultarPedidosResponse>> Consultar(Guid idCliente, CancellationToken cancellationToken)
    {
        List<Pedido> pedidos = await _pedidoRepository.ConsultarPedidos(idCliente, cancellationToken);

        List<ConsultarPedidosResponse> produtosDto = [];

        foreach (Pedido item in pedidos)
        {
            produtosDto.Add(new ConsultarPedidosResponse(item.Id, item.Produto!.Nome, item.Preco, item.DataCadastro, EnumDescricao.Exibir(item.SituacaoPedido)));
        }

        return produtosDto;
    }
}
