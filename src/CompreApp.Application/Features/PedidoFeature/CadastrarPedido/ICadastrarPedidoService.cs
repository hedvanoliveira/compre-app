using System.Threading.Tasks;
using System.Threading;
using System;

namespace CompreApp.Application.Features.PedidoFeature.CadastrarPedido;

public interface ICadastrarPedidoService
{
    Task<CadastrarPedidoResponse> CadastrarPedido(CadastrarPedidoRequest request, Guid idCliente, CancellationToken cancellationToken);
}
