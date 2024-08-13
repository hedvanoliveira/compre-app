using CompreApp.Domain.Enums;

namespace CompreApp.Domain.Entities;

public class PedidoPagamento: EntidadeBase
{
    public PedidoPagamento(){}
    public PedidoPagamento(Guid id, DateTime? dataPagamento, ClienteCartao? clienteCartao)
    {
        Id = id;
        DataPagamento = dataPagamento;
        ClienteCartao = clienteCartao;
        Situacao = SituacaoCadastroEnum.Ativo;
    }

    public DateTime? DataPagamento { get; private set; }
    public Guid PedidoId { get; private set; }
    public Pedido? Pedido { get; private set; }
    public Guid ClienteCartaoId { get; private set; }
    public ClienteCartao? ClienteCartao { get; private set; }
}