using CompreApp.Application.Common;
using CompreApp.Domain.Entities;
using CompreApp.Infra.Data.Repositories.Interfaces;
using CompreApp.Infra.Pagamento.Integracao;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompreApp.Application.Features.PedidoFeature.CadastrarPedido;

public class CadastrarPedidoService(IProdutoRepository produtoRepository, IClienteRepository clienteRepository, IPedidoRepository pedidoRepository, IIntegracaoPagamentoService integracaoPagamentoService, ILogger<CadastrarPedidoService> logger) : ICadastrarPedidoService
{
    private readonly IIntegracaoPagamentoService _integracaoPagamentoService = integracaoPagamentoService;
    private readonly IProdutoRepository _produtoRepository = produtoRepository;
    private readonly IClienteRepository _clienteRepository = clienteRepository;
    private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
    private readonly ILogger<CadastrarPedidoService> _logger = logger;

    public async Task<CadastrarPedidoResponse> CadastrarPedido(CadastrarPedidoRequest request, Guid idCliente, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Pedido: {request}", request);

        // Validação dos dados
        var resultadoValidacao = await ValidarPedido(request, idCliente, cancellationToken);

        if (!resultadoValidacao.Resultado.Sucesso)
        {
            return new CadastrarPedidoResponse(DateTime.UtcNow, resultadoValidacao.Resultado.Mensagem, null);
        }

        var cliente = resultadoValidacao.Cliente!;
        var produto = resultadoValidacao.Produto!;

        // Criar pedido
        var pedidoCadastrar = CriarPedido(request, idCliente, cliente, produto);

        await _pedidoRepository.SalvarPedido(pedidoCadastrar, cancellationToken);
        _logger.LogInformation("Pedido salvo: {id}", pedidoCadastrar.Id);

        // Efetuar pagamento na API
        var integracaoPagamento = await EfetuarPagamento(pedidoCadastrar, cancellationToken);

        // Alterar situação do pedido
        return await FinalizarPedido(pedidoCadastrar.Id, integracaoPagamento, produto, cancellationToken);
    }

    public async Task<(Resultado Resultado, Cliente? Cliente, Produto? Produto)> ValidarPedido(CadastrarPedidoRequest request, Guid idCliente, CancellationToken cancellationToken)
    {
        Cliente? cliente = await _clienteRepository.ConsultarPorId(idCliente, cancellationToken);
        if (cliente is null)
        {
            return (new Resultado("Cliente não encontrado", false), null, null);
        }

        Produto? produto = await _produtoRepository.ConsultaProdutoPorId(request.ProdutoId, cancellationToken);
        if (produto is null)
        {
            return (new Resultado("Produto não encontrado", false), null, null);
        }

        return (new Resultado("Validação bem-sucedida", true), cliente, produto);
    }

    public Pedido CriarPedido(CadastrarPedidoRequest request, Guid idCliente, Cliente cliente, Produto produto)
    {
        DateTime dataCadastro = DateTime.UtcNow;

        ClienteCartao clienteCartao = new(Guid.NewGuid(), request.Cartao.NomeProprietarioCartao, request.Cartao.NumeroCartao, request.Cartao.MesVencimento, request.Cartao.AnoVencimento, request.Cartao.CodigoSeguranca, request.Cartao.BandeiraCartao, idCliente, dataCadastro);

        PedidoPagamento pedidoPagamento = new(Guid.NewGuid(), null, clienteCartao);

        return new Pedido(Guid.NewGuid(), produto.Preco, idCliente, produto.Id, cliente.ClienteEnderecos.FirstOrDefault()!.Id, dataCadastro, pedidoPagamento);
    }

    public async Task<IntegracaoPagamentoServiceResponse> EfetuarPagamento(Pedido pedidoCadastrar, CancellationToken cancellationToken)
    {
        ClienteCartao? clienteCartao = pedidoCadastrar.PedidoPagamentos.First().ClienteCartao;

        if (clienteCartao != null)
        {
            IntegracaoPagamentoServiceRequest pagamentoRequisicao = new(pedidoCadastrar.Id.ToString(), pedidoCadastrar.Preco, clienteCartao.NomeProprietarioCartao, clienteCartao.NumeroCartao, clienteCartao.MesVencimento, clienteCartao.AnoVencimento, clienteCartao.NumeroCartao, clienteCartao.BandeiraCartao);

            return await _integracaoPagamentoService.Pagar(pagamentoRequisicao, cancellationToken);
        }

        throw new ArgumentException("Dados do cartão não encontrado");
    }

    public async Task<CadastrarPedidoResponse> FinalizarPedido(Guid pedidoId, IntegracaoPagamentoServiceResponse integracaoPagamento, Produto produto, CancellationToken cancellationToken)
    {
        Pedido? pedidoFinalizar = await _pedidoRepository.ConsultarPorId(pedidoId, cancellationToken);

        if (pedidoFinalizar != null)
        {
            pedidoFinalizar.AlterarSituacaoPedido(DateTime.UtcNow, integracaoPagamento.SituacaoPedido);
            await _pedidoRepository.AtualizarPedido(pedidoFinalizar, cancellationToken);
            _logger.LogInformation("Pedido finalizado: {SituacaoPedido}", integracaoPagamento.SituacaoPedido);
        }

        return new CadastrarPedidoResponse(DateTime.UtcNow, EnumDescricao.Exibir(integracaoPagamento.SituacaoPedido), new CadastrarPedidoDetalheResponse(pedidoId, produto.Preco, produto.Nome));
    }
}
