using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CompreApp.Application.Features.ClienteFeature.CadastrarCliente;

public class CadastrarClienteRequest
{
    [Required]
    [Length(6, 100)]
    public string Nome { get; set; }

    [SwaggerSchema(Description = "CPF valido, apenas números")]
    [Length(11, 11)]
    public string Cpf { get; set; }

    [Required]
    public DateOnly DataNascimento { get; set; }

    [SwaggerSchema(Description = "F ou M")]
    [Required]
    [MaxLength(1)]
    public string Sexo { get; set; }

    [SwaggerSchema(Description = "E-mail único")]
    [Required]
    [MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Length(6, 20)]
    public string Senha { get; set; }

    [Required]
    public CadastrarEnderecoRequest Endereco { get; set; }
}

public class CadastrarEnderecoRequest
{
    public string Logradouro { get; set; }
    [SwaggerSchema(Description = "Apenas números")]
    [Required]
    public string Cep { get; set; }

    [Required]
    [Length(2, 2)]
    public string Uf { get; set; }

    [Required]
    [Length(4, 100)]
    public string Municipio { get; set; }

    [Required]
    [Length(4, 100)]
    public string Bairro { get; set; }

    [MaxLength(10)]
    public string Numero { get; set; }

    [MaxLength(10)]
    public string Complemento { get; set; }
}

