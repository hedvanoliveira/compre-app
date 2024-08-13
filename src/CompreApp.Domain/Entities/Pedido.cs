using CompreApp.Domain.Enums;

namespace CompreApp.Domain.Entities;

public class Pedido: EntidadeBase
{
    
    public Pedido(){}
    public Pedido(Guid id, decimal preco, Guid clienteId, Guid produtoId, Guid clienteEnderecoId, DateTime dataCadastro, PedidoPagamento pedidoPagamento)
    {
        Id = id;
        Preco = preco;
        ClienteId = clienteId;
        ProdutoId = produtoId;
        ClienteEnderecoId = clienteEnderecoId;
        SituacaoPedido =  SituacaoPedidoEnum.AguardandoPagamento;
        PedidoPagamentos = [pedidoPagamento];
        DataCadastro = dataCadastro;
        Situacao = SituacaoCadastroEnum.Ativo;

        if (preco <= 0)
        {
            throw new ArgumentException("O Preço deve ser maior que zero");
        }
    }

    public void AlterarSituacaoPedido(DateTime dataAlteracao, SituacaoPedidoEnum situacaoPedido)
    {
        DataFinalizacaoPedido = dataAlteracao;
        SituacaoPedido = situacaoPedido;

        if(situacaoPedido == SituacaoPedidoEnum.AguardandoPagamento)
        {
            throw new ArgumentException("A situação do pedido deve ser diferente de Aguardando Pagamento");
        }
    }

    public DateTime? DataFinalizacaoPedido { get; private set; }
    public decimal Preco { get; private set; }

    public Guid ClienteId { get; private set; }
    public Cliente? Cliente { get; private set; }

    public Guid ProdutoId { get; private set; }
    public Produto? Produto { get; private set; }

    public Guid ClienteEnderecoId { get; private set; }
    public ClienteEndereco? ClienteEndereco { get; private set; }
    public SituacaoPedidoEnum SituacaoPedido { get; private set; }
    public List<PedidoPagamento> PedidoPagamentos { get; private set; }

}