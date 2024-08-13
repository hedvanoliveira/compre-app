using CompreApp.Domain.Entities;
using CompreApp.Domain.Enums;
using CompreApp.Infra.Data.Context;
using CompreApp.Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompreApp.Infra.Data.Repositories;

public class ProdutoRepository(ApiDbContext apiDbContext) : IProdutoRepository
{
    private readonly ApiDbContext _db = apiDbContext;

    public async Task<List<Produto>> ConsultarProdutos(CancellationToken cancellationToken)
    {
        return await _db.Produtos
               .AsNoTracking()
               .Where(x => x.Situacao == SituacaoCadastroEnum.Ativo)
               .ToListAsync(cancellationToken);
    }

    public async Task<Produto?> ConsultaProdutoPorId(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Produtos
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == id && x.Situacao == SituacaoCadastroEnum.Ativo, cancellationToken);
    }
}
