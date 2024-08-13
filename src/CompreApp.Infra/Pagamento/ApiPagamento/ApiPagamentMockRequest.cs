namespace CompreApp.Infra.GatewayPagamento.ApiPagamento;

public record ApiPagamentMockRequest(string CodigoPagamento, decimal Preco, string NomeProprietarioCartao, string NumeroCartao, string MesVencimento, string AnoVencimento, string CodigoSeguranca, string BandeiraCartao);
