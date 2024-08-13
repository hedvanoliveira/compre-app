using CompreApp.Application.Common;
using CompreApp.Application.Features.ClienteFeature.AutenticarCliente;
using FluentValidation;

namespace CompreApp.Application.Features.ClienteFeature.CadastrarCliente;

public class AutenticarClienteRequestValidator : AbstractValidator<AutenticarClienteRequest>
{
    public AutenticarClienteRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Senha).MinimumLength(6).MaximumLength(20);
    }
}