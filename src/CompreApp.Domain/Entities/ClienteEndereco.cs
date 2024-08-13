using CompreApp.Domain.Enums;

namespace CompreApp.Domain.Entities;

public class ClienteEndereco : EntidadeBase
{
    public ClienteEndereco() { }
    public ClienteEndereco(Guid id, string logradouro, string cep, string uf, string municipio, string bairro, string numero, string complemento, DateTime dataCadastro)
    {
        Id = id;
        Logradouro = logradouro;
        Cep = cep;
        Uf = uf;
        Municipio = municipio;
        Bairro = bairro;
        Numero = numero;
        Complemento = complemento;
        DataCadastro = dataCadastro;
        Situacao = SituacaoCadastroEnum.Ativo;
    }

    public string Logradouro { get; private set; }
    public string Cep { get; private set; }
    public string Uf { get; private set; }
    public string Municipio { get; private set; }
    public string Bairro { get; private set; }
    public string Numero { get; private set; }
    public string Complemento { get; private set; }
    public Guid ClienteId { get; private set; }
    public Cliente? Cliente { get; private set; }
}