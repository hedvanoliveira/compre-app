using CompreApp.Domain.Enums;

namespace CompreApp.Domain.Entities;

public class Produto: EntidadeBase
{
    public Produto(){}

    public Produto(Guid id, string nome, string descricao, decimal preco, DateTime dataCadastro, SituacaoCadastroEnum situacaoCadastro)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        DataCadastro = dataCadastro;
        Situacao = situacaoCadastro;
    }

    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Preco { get; private set; }
}