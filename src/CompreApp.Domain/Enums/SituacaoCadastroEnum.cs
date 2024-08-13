using System.ComponentModel;

namespace CompreApp.Domain.Enums;
public enum SituacaoCadastroEnum
{
    [Description("Excluido")]
    Excluido = 0,
    [Description("Ativo")]
    Ativo = 1,
    [Description("Inativo")]
    Inativo = 3,
}
