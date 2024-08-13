namespace CompreApp.Infra.Pagamento.Integracao;

public abstract class IntegracaoPagamentoBase
{
    abstract public Task<IntegracaoPagamentoServiceResponse> Pagar(IntegracaoPagamentoServiceRequest pagamentoRequisicao, CancellationToken cancellationToken);
}
