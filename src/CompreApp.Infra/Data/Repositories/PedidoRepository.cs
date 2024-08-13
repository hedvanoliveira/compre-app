using CompreApp.Domain.Entities;
using CompreApp.Domain.Enums;
using CompreApp.Infra.Data.Context;
using CompreApp.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompreApp.Infra.Data.Repositories;

public class PedidoRepository(ApiDbContext apiDbContext) : IPedidoRepository
{
    private readonly ApiDbContext _db = apiDbContext;

    public async Task<Pedido?> ConsultarPorId(Guid id, CancellationToken cancellationToken)
    {
        Pedido? cliente = await _db.Pedidos
                           .Include(x=>x.Produto)
                           .Where(x => x.Id == id && x.Situacao == Domain.Enums.SituacaoCadastroEnum.Ativo)
                           .FirstOrDefaultAsync(cancellationToken);

        return cliente;
    }

    public async Task SalvarPedido(Pedido pedido, CancellationToken cancellationToken)
    {
        _db.Pedidos.Add(pedido);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarPedido(Pedido pedido, CancellationToken cancellationToken)
    {
        _db.Update(pedido);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Pedido>> ConsultarPedidos(Guid idCliente, CancellationToken cancellationToken)
    {
        return await _db.Pedidos
               .AsNoTracking()
               .Include(x => x.Cliente)
               .Include(x => x.Produto)
               .Where(x => x.ClienteId == idCliente && x.Situacao == SituacaoCadastroEnum.Ativo)
               .OrderByDescending(x => x.DataCadastro)
               .ToListAsync(cancellationToken);
    }
}
