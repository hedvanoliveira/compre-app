using CompreApp.Domain.Entities;

namespace CompreApp.Infra.Data.Repositories.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> ConsultarPorEmail(string email, CancellationToken cancellationToken);
    Task<Cliente?> ConsultarPorId(Guid id, CancellationToken cancellationToken);
    Task SalvarCliente(Cliente cliente, CancellationToken cancellationToken);
    Task<bool> VerificarEmailExiste(string email, CancellationToken cancellationToken);
}