namespace CompreApp.Infra.Pagamento.Integracao;
public interface IIntegracaoPagamentoService
{
    Task<IntegracaoPagamentoServiceResponse> Pagar(IntegracaoPagamentoServiceRequest request, CancellationToken cancellationToken);
}