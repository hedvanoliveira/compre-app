using CompreApp.Application.Common;
using CompreApp.Domain.Entities;
using CompreApp.Infra.Data.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompreApp.Application.Features.ClienteFeature.CadastrarCliente;

public class CadastrarClienteService(IClienteRepository clienteRepository) : ICadastrarClienteService
{
    private readonly IClienteRepository _clienteRepository = clienteRepository;
    public async Task<CadastrarClienteResponse> CadastrarCliente(CadastrarClienteRequest request, CancellationToken cancellationToken)
    {
        if(await _clienteRepository.VerificarEmailExiste(request.Email, cancellationToken))
        {
            return new CadastrarClienteResponse(DateTime.UtcNow, "E-mail já cadastrado", null);
        }

        DateTime dataCadastro = DateTime.UtcNow;
        string senhaCriptografada = CriptografiaSenha.Criptografar(request.Senha);

        ClienteEndereco clienteEndereco = new(Guid.NewGuid(), request.Endereco.Logradouro, request.Endereco.Cep, request.Endereco.Uf, request.Endereco.Municipio, request.Endereco.Bairro, request.Endereco.Numero, request.Endereco.Complemento, dataCadastro);
        Cliente cliente = new(Guid.NewGuid(), request.Nome, request.Cpf, request.DataNascimento, request.Sexo, request.Email, senhaCriptografada, dataCadastro, clienteEndereco);

        await _clienteRepository.SalvarCliente(cliente, cancellationToken);

        return new CadastrarClienteResponse(DateTime.UtcNow, "Cliente cadastrado com sucesso", new CadastrarClienteResponseDetalheResponse(cliente.Id, cliente.Nome, cliente.Email));
    }
}
