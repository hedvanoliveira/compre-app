using CompreApp.Application.Common;
using FluentValidation;

namespace CompreApp.Application.Features.ClienteFeature.CadastrarCliente;

public class CadastrarClienteRequestValidator : AbstractValidator<CadastrarClienteRequest>
{
    public CadastrarClienteRequestValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().MinimumLength(6).MaximumLength(100);
        RuleFor(x => x.Cpf).NotEmpty().Length(11).Must(ValicacaoCpf.CpfValido).WithMessage("O CPF é inválido");
        RuleFor(x => x.DataNascimento).NotEmpty();
        RuleFor(x => x.Sexo).NotEmpty().Length(1).Must(g => g == "F" || g == "M").WithMessage("O sexo deve ser 'F' ou 'M'");
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Senha).MinimumLength(6).MaximumLength(20);

        RuleFor(x => x.Endereco.Logradouro).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Endereco.Cep).NotEmpty().Length(8);
        RuleFor(x => x.Endereco.Uf).NotEmpty().Length(2);
        RuleFor(x => x.Endereco.Municipio).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Endereco.Bairro).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Endereco.Numero).MaximumLength(10);
        RuleFor(x => x.Endereco.Complemento).MaximumLength(100);
    }
}