namespace CompreApp.Infra.Pagamento.Integracao;

public record IntegracaoPagamentoServiceRequest(string CodigoPagamento, decimal Preco, string NomeProprietarioCartao, string NumeroCartao, string MesVencimento, string AnoVencimento, string CodigoSeguranca, string BandeiraCartao);
