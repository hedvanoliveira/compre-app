using CompreApp.Domain.Enums;
using CompreApp.Infra.Common;
using CompreApp.Infra.Pagamento.Integracao;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace CompreApp.Infra.GatewayPagamento.ApiPagamento;

public class ApiPagamentoMock(IHttpClientFactory httpClientFactory, IOptions<ConfigSettings> configSettings, ILogger<ApiPagamentoMock> _logger) : IntegracaoPagamentoBase, IIntegracaoPagamentoService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public override async Task<IntegracaoPagamentoServiceResponse> Pagar(IntegracaoPagamentoServiceRequest request, CancellationToken cancellationToken)
    {
        ApiPagamentMockRequest pagamentoWireMockRequest = new(request.CodigoPagamento, request.Preco, request.NomeProprietarioCartao, request.NumeroCartao, request.MesVencimento, request.AnoVencimento, request.CodigoSeguranca, request.BandeiraCartao);

        var json = new StringContent(
                        JsonSerializer.Serialize(pagamentoWireMockRequest),
                        Encoding.UTF8,
                        MediaTypeNames.Application.Json);

        var httpClient = _httpClientFactory.CreateClient();

        _logger.LogInformation("POST api de pagamento: {json}", json);

        // Definindo a política de retry com Polly
        var retryPolicy = Policy.Handle<HttpRequestException>()
                                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                                    onRetry: (outcome, timespan, retryAttempt, context) =>
                                    {
                                        _logger.LogWarning("Tentativa {retryAttempt} falhou. Tentando novamente em {Seconds} segundos.", retryAttempt, timespan.Seconds);
                                    });

        var response = await retryPolicy.ExecuteAsync(async () =>
        {
            return await httpClient.PostAsync($"{configSettings.Value.UrlGatewayPagamento}/pagamento-cartao", json, cancellationToken);
        });

        ApiPagamentoMockResponse? pagamentoResposta = await response.Content.ReadFromJsonAsync<ApiPagamentoMockResponse>(cancellationToken);

        _logger.LogInformation("Resposta do POST api de pagamento: {pagamentoResposta}", pagamentoResposta);

        if (pagamentoResposta != null)
        {
            if (pagamentoResposta.Codigo == (int)SituacaoPedidoEnum.Pago)
            {
                return new IntegracaoPagamentoServiceResponse(pagamentoResposta.Situacao, SituacaoPedidoEnum.Pago);
            }
            else if (pagamentoResposta.Codigo == (int)SituacaoPedidoEnum.NaoAutorizado)
            {
                return new IntegracaoPagamentoServiceResponse(pagamentoResposta.Situacao, SituacaoPedidoEnum.NaoAutorizado);
            }
            else
            {
                return new IntegracaoPagamentoServiceResponse(pagamentoResposta.Situacao, SituacaoPedidoEnum.ErroNoPagamento);
            }
        }

        return new IntegracaoPagamentoServiceResponse("Erro na integração de pagamento", SituacaoPedidoEnum.ErroNoPagamento);
    }
}
