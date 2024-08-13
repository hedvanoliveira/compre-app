namespace CompreApp.Domain.Entities;

public class ClienteCartao : EntidadeBase
{
    public ClienteCartao(){}
    public ClienteCartao(Guid id, string nomeProprietarioCartao, string numeroCartao, string mesVencimento, string anoVencimento, string codigoSeguranca, string bandeiraCartao, Guid clienteId, DateTime dataCadastro)
    {
        Id = id;
        NomeProprietarioCartao = nomeProprietarioCartao;
        NumeroCartao = numeroCartao;
        MesVencimento = mesVencimento;
        AnoVencimento = anoVencimento;
        CodigoSeguranca = codigoSeguranca;
        BandeiraCartao = bandeiraCartao;
        DataCadastro = dataCadastro;
        Situacao = Enums.SituacaoCadastroEnum.Ativo;
        ClienteId = clienteId;
    }

    public string NomeProprietarioCartao { get; private set; }
    public string NumeroCartao { get; private set; }
    public string MesVencimento { get; private set; }
    public string AnoVencimento { get; private set; }
    public string CodigoSeguranca { get; private set; }
    public string BandeiraCartao { get; private set; }
    public Guid ClienteId { get; private set; }
    public Cliente? Cliente { get; private set; }
}