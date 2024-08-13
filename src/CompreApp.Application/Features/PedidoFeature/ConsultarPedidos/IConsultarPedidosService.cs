using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompreApp.Application.Features.PedidoFeature.ConsultarPedidos;

public interface IConsultarPedidosService
{
    Task<List<ConsultarPedidosResponse>> Consultar(Guid idCliente, CancellationToken cancellationToken);
}
