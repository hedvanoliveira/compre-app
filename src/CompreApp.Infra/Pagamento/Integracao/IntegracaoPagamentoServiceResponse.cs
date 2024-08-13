using CompreApp.Domain.Enums;

namespace CompreApp.Infra.Pagamento.Integracao;
public record IntegracaoPagamentoServiceResponse(string Situacao, SituacaoPedidoEnum SituacaoPedido);