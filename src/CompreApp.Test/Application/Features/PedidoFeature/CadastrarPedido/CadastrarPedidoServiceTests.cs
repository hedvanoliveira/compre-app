using CompreApp.Application.Features.PedidoFeature.CadastrarPedido;
using CompreApp.Domain.Entities;
using CompreApp.Domain.Enums;
using CompreApp.Infra.Data.Repositories.Interfaces;
using CompreApp.Infra.GatewayPagamento;
using CompreApp.Infra.Pagamento.Integracao;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CompreApp.Test.Application.Features.PedidoFeature.CadastrarPedido;

public class CadastrarPedidoServiceTests
{
    private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
    private readonly Mock<IIntegracaoPagamentoService> _integracaoPagamentoServiceMock;
    private readonly Mock<ILogger<CadastrarPedidoService>> _loggerMock;
    private readonly CadastrarPedidoService _service;

    public CadastrarPedidoServiceTests()
    {
        _produtoRepositoryMock = new Mock<IProdutoRepository>();
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _pedidoRepositoryMock = new Mock<IPedidoRepository>();
        _integracaoPagamentoServiceMock = new Mock<IIntegracaoPagamentoService>();
        _loggerMock = new Mock<ILogger<CadastrarPedidoService>>();

        _service = new CadastrarPedidoService(
            _produtoRepositoryMock.Object,
            _clienteRepositoryMock.Object,
            _pedidoRepositoryMock.Object,
            _integracaoPagamentoServiceMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CadastrarPedido_DeveRetornarErro_QuandoClienteNaoEncontrado()
    {
        // Arrange
        var request = new CadastrarPedidoRequest { ProdutoId = Guid.NewGuid() };
        var idCliente = Guid.NewGuid();

        _clienteRepositoryMock.Setup(r => r.ConsultarPorId(idCliente, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Cliente?)null);

        // Act
        var result = await _service.CadastrarPedido(request, idCliente, CancellationToken.None);

        // Assert
        Assert.Null(result.Detalhes);
        Assert.Equal("Cliente não encontrado", result.SituacaoPedido);
    }

    [Fact]
    public async Task CadastrarPedido_DeveRetornarErro_QuandoProdutoNaoEncontrado()
    {
        // Arrange
        var request = new CadastrarPedidoRequest { ProdutoId = Guid.NewGuid() };
        var idCliente = Guid.NewGuid();
        var cliente = new Cliente(); // Substitua por uma instância válida

        _clienteRepositoryMock.Setup(r => r.ConsultarPorId(idCliente, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cliente);

        _produtoRepositoryMock.Setup(r => r.ConsultaProdutoPorId(request.ProdutoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Produto?)null);

        // Act
        var result = await _service.CadastrarPedido(request, idCliente, CancellationToken.None);

        // Assert
        Assert.Null(result.Detalhes);
        Assert.Equal("Produto não encontrado", result.SituacaoPedido);
    }

    [Fact]
    public async Task CadastrarPedido_DeveRetornarSucesso_QuandoRequisicaoValida()
    {
        // Arrange
        var request = new CadastrarPedidoRequest() { ProdutoId = Guid.NewGuid(), Cartao = new CadastrarPedidoCartaoRequest() { NomeProprietarioCartao = "", NumeroCartao = "", MesVencimento = "", AnoVencimento = "", CodigoSeguranca = "", BandeiraCartao = ""} };
        var idCliente = Guid.NewGuid();
        var cliente = new Cliente(idCliente, "", "", new DateOnly(1990, 01, 01), "", "", "", DateTime.UtcNow, new ClienteEndereco(Guid.NewGuid(), "", "", "", "", "", "", "", DateTime.UtcNow));
        var produto = new Produto(Guid.NewGuid(), "", "", 250, DateTime.UtcNow, SituacaoCadastroEnum.Ativo);
        var pedidoId = Guid.NewGuid();
        var integracaoResponse = new IntegracaoPagamentoServiceResponse("Pago", SituacaoPedidoEnum.Pago);

        _clienteRepositoryMock.Setup(r => r.ConsultarPorId(idCliente, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cliente);

        _produtoRepositoryMock.Setup(r => r.ConsultaProdutoPorId(request.ProdutoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(produto);

        _pedidoRepositoryMock.Setup(r => r.SalvarPedido(It.IsAny<Pedido>(), It.IsAny<CancellationToken>()))
            .Callback<Pedido, CancellationToken>((pedido, token) => pedido.Id = pedidoId)
            .Returns(Task.CompletedTask);

        _integracaoPagamentoServiceMock.Setup(s => s.Pagar(It.IsAny<IntegracaoPagamentoServiceRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(integracaoResponse);

        _pedidoRepositoryMock.Setup(r => r.ConsultarPorId(pedidoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Pedido { Id = pedidoId });

        // Act
        var result = await _service.CadastrarPedido(request, idCliente, CancellationToken.None);

        // Assert
        Assert.NotNull(result.Detalhes);
        Assert.Equal("Compra confirmada", result.SituacaoPedido);
        Assert.Equal(pedidoId, result.Detalhes!.CodigoPedido);
    }
}