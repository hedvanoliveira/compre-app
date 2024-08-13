using CompreApp.Domain.Entities;

namespace CompreApp.Infra.Data.Repositories.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> ConsultarPorId(Guid id, CancellationToken cancellationToken);
    Task SalvarPedido(Pedido pedido, CancellationToken cancellationToken);
    Task AtualizarPedido(Pedido pedido, CancellationToken cancellationToken);
    Task<List<Pedido>> ConsultarPedidos(Guid idCliente, CancellationToken cancellationToken);
}