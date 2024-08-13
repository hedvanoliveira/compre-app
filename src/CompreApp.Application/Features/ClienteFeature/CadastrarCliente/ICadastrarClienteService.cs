using System.Threading;
using System.Threading.Tasks;

namespace CompreApp.Application.Features.ClienteFeature.CadastrarCliente;

public interface ICadastrarClienteService
{
    Task<CadastrarClienteResponse> CadastrarCliente(CadastrarClienteRequest request, CancellationToken cancellationToken);
}
