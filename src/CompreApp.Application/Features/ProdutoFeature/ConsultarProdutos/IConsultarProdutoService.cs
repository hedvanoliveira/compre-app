using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompreApp.Application.Features.ProdutoFeature.ConsultarProdutos;

public interface IConsultarProdutoService
{
    Task<List<ConsultarProdutoResponse>> Consultar(CancellationToken cancellationToken);
}
