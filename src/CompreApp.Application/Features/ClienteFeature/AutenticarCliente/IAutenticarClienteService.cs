using System.Threading;
using System.Threading.Tasks;

namespace CompreApp.Application.Features.ClienteFeature.AutenticarCliente;

public interface IAutenticarClienteService
{
    Task<AutenticarClienteResponse?> Autenticar(AutenticarClienteRequest request, CancellationToken cancellationToken);
}
