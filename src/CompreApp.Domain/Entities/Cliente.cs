using CompreApp.Domain.Enums;

namespace CompreApp.Domain.Entities;

public class Cliente : EntidadeBase
{
    public Cliente() { }
    public Cliente(Guid id, string nome, string cpf, DateOnly dataNascimento, string sexo, string email, string senha, DateTime dataCadastro, ClienteEndereco clienteEndereco)
    {
        Id = id;
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Sexo = sexo.ToUpper();
        Email = email.ToLower();
        Senha = senha;
        DataCadastro = dataCadastro;
        Situacao = SituacaoCadastroEnum.Ativo;
        ClienteEnderecos = [clienteEndereco];

        if (clienteEndereco == null)
        {
            throw new ArgumentException("Endereço obrigatório");
        }
    }

    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public DateOnly DataNascimento { get; private set; }
    public string Sexo { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }

    public List<ClienteEndereco> ClienteEnderecos { get; private set; }
    public List<ClienteCartao> ClienteCartoes { get; private set; }
    public List<Pedido> Pedidos { get; private set; }
}