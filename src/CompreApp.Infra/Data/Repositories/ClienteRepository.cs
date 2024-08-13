using CompreApp.Domain.Entities;
using CompreApp.Infra.Data.Context;
using CompreApp.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompreApp.Infra.Data.Repositories;

public class ClienteRepository(ApiDbContext apiDbContext) : IClienteRepository
{
    private readonly ApiDbContext _db = apiDbContext;

    public async Task<Cliente?> ConsultarPorEmail(string email, CancellationToken cancellationToken)
    {
        Cliente? cliente = await _db.Clientes
                           .AsNoTracking()
                           .Where(x => x.Email == email && x.Situacao == Domain.Enums.SituacaoCadastroEnum.Ativo)
                           .FirstOrDefaultAsync(cancellationToken);

        return cliente;
    }

    public async Task<Cliente?> ConsultarPorId(Guid id, CancellationToken cancellationToken)
    {
        Cliente? cliente = await _db.Clientes
                           .AsNoTracking()
                           .Include(x => x.ClienteEnderecos)
                           .Where(x => x.Id == id && x.Situacao == Domain.Enums.SituacaoCadastroEnum.Ativo)
                           .FirstOrDefaultAsync(cancellationToken);

        return cliente;
    }

    public async Task<bool> VerificarEmailExiste(string email, CancellationToken cancellationToken)
    {
        return await _db.Clientes.AsNoTracking().AnyAsync(x => x.Email == email && x.Situacao == Domain.Enums.SituacaoCadastroEnum.Ativo, cancellationToken);
    }

    public async Task SalvarCliente(Cliente cliente, CancellationToken cancellationToken)
    {
        _db.Clientes.Add(cliente);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
