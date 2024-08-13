using CompreApp.Domain.Entities;

namespace CompreApp.Infra.Data.Repositories.Interfaces;

public interface IProdutoRepository
{
    Task<List<Produto>> ConsultarProdutos(CancellationToken cancellationToken);
    Task<Produto?> ConsultaProdutoPorId(Guid id, CancellationToken cancellationToken);
}
