using CompreApp.Domain.Entities;
using CompreApp.Infra.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompreApp.Application.Features.ProdutoFeature.ConsultarProdutos;

public class ConsultarProdutoService(IProdutoRepository produtoRepository) : IConsultarProdutoService
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository;

    public async Task<List<ConsultarProdutoResponse>> Consultar(CancellationToken cancellationToken)
    {
        List<Produto> produtos = await _produtoRepository.ConsultarProdutos(cancellationToken);

        List<ConsultarProdutoResponse> produtosDto = [];

        foreach (Produto item in produtos)
        {
            produtosDto.Add(new ConsultarProdutoResponse(item.Id, item.Nome, item.Descricao));
        }

        return produtosDto;
    }
}
