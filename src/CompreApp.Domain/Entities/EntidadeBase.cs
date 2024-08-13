using CompreApp.Domain.Enums;

namespace CompreApp.Domain.Entities;

public class EntidadeBase
{
    public Guid Id { get; set; }
    public DateTime DataCadastro { get; internal set; }
    public DateTime? DataAlteracao { get; internal set; }
    public SituacaoCadastroEnum Situacao { get; internal set; }
}