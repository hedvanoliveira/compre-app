using System.ComponentModel;

namespace CompreApp.Domain.Enums;
public enum SituacaoPedidoEnum
{
    [Description("Aguardando Pagamento")]
    AguardandoPagamento = 1,
    [Description("Compra confirmada")]
    Pago = 2,
    [Description("Cartão não autorizado")]
    NaoAutorizado = 3,
    [Description("Erro no pagamento")]
    ErroNoPagamento = 4,
}