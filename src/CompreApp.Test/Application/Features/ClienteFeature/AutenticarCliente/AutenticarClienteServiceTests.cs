using CompreApp.Application.Common;
using CompreApp.Application.Features.ClienteFeature.AutenticarCliente;
using CompreApp.Domain.Entities;
using CompreApp.Infra.Common;
using CompreApp.Infra.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace CompreApp.Test.Application.Features.ClienteFeature.AutenticarCliente;

public class AutenticarClienteServiceTests
{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly Mock<IOptions<ConfigSettings>> _configSettingsMock;
    private readonly Mock<ILogger<AutenticarClienteService>> _loggerMock;
    private readonly IAutenticarClienteService _autenticarClienteService;

    public AutenticarClienteServiceTests()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _configSettingsMock = new Mock<IOptions<ConfigSettings>>();
        _loggerMock = new Mock<ILogger<AutenticarClienteService>>();

        // Configurando valores simulados de ConfigSettings
        var configSettings = new ConfigSettings
        {
            JwtChave = "ac06d49e-f028-476e-9818-4edfe85f6d9f",
            JwtExpiracaoMinutos = "60"
        };

        _configSettingsMock.Setup(cs => cs.Value).Returns(configSettings);

        _autenticarClienteService = new AutenticarClienteService(
            _clienteRepositoryMock.Object,
            _configSettingsMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Autenticar_EmailEsenhaCorretos_DeveRetornarAutenticarClienteResponse()
    {
        // Arrange
        var request = new AutenticarClienteRequest
        {
            Email = "josesilva@gmail.com",
            Senha = "123456"
        };

        var cliente = new Cliente(Guid.NewGuid(), "JOSE DA SILVA", "55143529000", new DateOnly(1990, 01, 01), "M", "josesilva@gmail.com", "vbbaSKhqV/6aPsCbij30ynyMWDrcuWmhoQORcgLonoet6H6pvGDSJE4GvKtdJ8V7", DateTime.UtcNow, new ClienteEndereco(Guid.Empty, null, null, null, null, null, null, null, DateTime.UtcNow));

        _clienteRepositoryMock
            .Setup(repo => repo.ConsultarPorEmail(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cliente);

        // Act
        var response = await _autenticarClienteService.Autenticar(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(cliente.Id, response.Id);
        Assert.Equal(cliente.Nome, response.Nome);
        Assert.Equal(cliente.Email, response.Email);
        Assert.False(string.IsNullOrEmpty(response.Token));
    }

    [Fact]
    public async Task Autenticar_EmailOuSenhaIncorretos_DeveRetornarNull()
    {
        // Arrange
        var request = new AutenticarClienteRequest
        {
            Email = "josesilva@gmail.com",
            Senha = "SenhaErrada"
        };

        var cliente = new Cliente(Guid.NewGuid(), "JOSE DA SILVA", "55143529000", new DateOnly(1990, 01, 01), "M", "josesilva@gmail.com", "vbbaSKhqV/6aPsCbij30ynyMWDrcuWmhoQORcgLonoet6H6pvGDSJE4GvKtdJ8V7", DateTime.UtcNow, new ClienteEndereco(Guid.Empty, null, null, null, null, null, null, null, DateTime.UtcNow));

        _clienteRepositoryMock
            .Setup(repo => repo.ConsultarPorEmail(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cliente);

        // Act
        var response = await _autenticarClienteService.Autenticar(request, CancellationToken.None);

        // Assert
        Assert.Null(response);
    }
}