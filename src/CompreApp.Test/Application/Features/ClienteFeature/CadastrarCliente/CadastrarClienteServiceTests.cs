using CompreApp.Application.Features.ClienteFeature.CadastrarCliente;
using CompreApp.Domain.Entities;
using CompreApp.Infra.Data.Repositories.Interfaces;
using Moq;

namespace CompreApp.Test.Application.Features.ClienteFeature.CadastrarCliente;

public class CadastrarClienteServiceTests
{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly ICadastrarClienteService _cadastrarClienteService;

    public CadastrarClienteServiceTests()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _cadastrarClienteService = new CadastrarClienteService(_clienteRepositoryMock.Object);
    }

    [Fact]
    public async Task CadastrarCliente_EmailJaCadastrado_DeveRetornarErro()
    {
        // Arrange
        var request = new CadastrarClienteRequest
        {
            Nome = "JOSE DA SILVA",
            Email = "josesilva@gmail.com",
            Senha = "Senha123456",
            Cpf = "55143529000",
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = "M",
            Endereco = new CadastrarEnderecoRequest
            {
                Logradouro = "RUA DAS OLIVEIRAS",
                Cep = "12345678",
                Uf = "SP",
                Municipio = "SÃO PAULO",
                Bairro = "CENTRO",
                Numero = "120",
                Complemento = "APTO 210"
            }
        };

        _clienteRepositoryMock
            .Setup(repo => repo.VerificarEmailExiste(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var resultado = await _cadastrarClienteService.CadastrarCliente(request, CancellationToken.None);

        // Assert
        Assert.Null(resultado.Detalhes);
        Assert.Equal("E-mail já cadastrado", resultado.Situacao);
    }

    [Fact]
    public async Task CadastrarCliente_EmailNaoCadastrado_DeveCadastrarCliente()
    {
        // Arrange
        var request = new CadastrarClienteRequest
        {
            Nome = "JOSE DA SILVA",
            Email = "josesilva@gmail.com",
            Senha = "Senha123456",
            Cpf = "55143529000",
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = "M",
            Endereco = new CadastrarEnderecoRequest
            {
                Logradouro = "RUA DAS OLIVEIRAS",
                Cep = "12345678",
                Uf = "SP",
                Municipio = "SÃO PAULO",
                Bairro = "CENTRO",
                Numero = "120",
                Complemento = "APTO 210"
            }
        };

        _clienteRepositoryMock
            .Setup(repo => repo.VerificarEmailExiste(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _clienteRepositoryMock
            .Setup(repo => repo.SalvarCliente(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var resultado = await _cadastrarClienteService.CadastrarCliente(request, CancellationToken.None);

        // Assert
        Assert.NotNull(resultado.Detalhes);
        Assert.Equal("Cliente cadastrado com sucesso", resultado.Situacao);

        _clienteRepositoryMock.Verify(repo => repo.SalvarCliente(It.IsAny<Cliente>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}