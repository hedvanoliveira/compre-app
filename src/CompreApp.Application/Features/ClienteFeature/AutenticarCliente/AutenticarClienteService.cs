using CompreApp.Application.Common;
using CompreApp.Domain.Entities;
using CompreApp.Infra.Common;
using CompreApp.Infra.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompreApp.Application.Features.ClienteFeature.AutenticarCliente;

public class AutenticarClienteService(IClienteRepository clienteRepository, IOptions<ConfigSettings> configSettings, ILogger<AutenticarClienteService> _logger) : IAutenticarClienteService
{
    private readonly IClienteRepository _clienteRepository = clienteRepository;
    public async Task<AutenticarClienteResponse?> Autenticar(AutenticarClienteRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Autenticar email: {Email}", request.Email);

        Cliente? cliente = await _clienteRepository.ConsultarPorEmail(request.Email, cancellationToken);

        if (cliente != null && CriptografiaSenha.Verificar(request.Senha, cliente.Senha))
        {
            string token = JwtBearerService.GenerateToken(cliente.Id.ToString(), cliente.Nome, cliente.Email, configSettings.Value.JwtChave, DateTime.UtcNow.AddHours(int.Parse(configSettings.Value.JwtExpiracaoMinutos)));

            _logger.LogInformation("Autenticar email: {Email} encontrado", request.Email);

            return new AutenticarClienteResponse(cliente.Id, cliente.Nome, cliente.Email, token);
        }

        _logger.LogInformation("Autenticar email: {Email} não encontrado", request.Email);

        return null;
    }
}
