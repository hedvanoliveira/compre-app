using CompreApp.Application.Features.PedidoFeature.CadastrarPedido;
using FluentValidation;
using System;

namespace CompreApp.Application.Features.ClienteFeature.CadastrarCliente;

public class CadastrarPedidoRequestValidator : AbstractValidator<CadastrarPedidoRequest>
{
    public CadastrarPedidoRequestValidator()
    {
        RuleFor(x => x.ProdutoId).NotEmpty();

        RuleFor(x => x.Cartao.NomeProprietarioCartao).NotEmpty().MinimumLength(6).MaximumLength(50);
        RuleFor(x => x.Cartao.NumeroCartao).NotEmpty().MinimumLength(14).MaximumLength(16).Matches(@"^\d+$").WithMessage("Informe apenas números");
        RuleFor(x => x.Cartao.MesVencimento).NotEmpty().Length(2).Must(BeAValidMonth).WithMessage("O mês deve estar entre 1 e 12");
        RuleFor(x => x.Cartao.AnoVencimento).NotEmpty().Length(4).Must(BeAValidYear).WithMessage("Ano inválido");
        RuleFor(x => x.Cartao.CodigoSeguranca).NotEmpty().MinimumLength(3).MaximumLength(4).Matches(@"^\d+$").WithMessage("Informe apenas números");
        RuleFor(x => x.Cartao.BandeiraCartao).NotEmpty().MaximumLength(10);

        RuleFor(x => new { x.Cartao.MesVencimento, x.Cartao.AnoVencimento })
           .Custom((x, context) =>
           {
               if (!VerificarDataMaiorQueAtual(x.MesVencimento, x.AnoVencimento))
               {
                   context.AddFailure("Data Vencimento", "A data vencimento deve ser maior que a data atual.");
               }
           });
    }

    private bool BeAValidMonth(string mes)
    {
        return int.TryParse(mes, out int mesInt) && mesInt >= 1 && mesInt <= 12;
    }

    private bool BeAValidYear(string ano)
    {
        return int.TryParse(ano, out _);
    }

    private bool VerificarDataMaiorQueAtual(string mes, string ano)
    {
        if (!int.TryParse(mes, out int mesInt) || !int.TryParse(ano, out int anoInt))
        {
            return false;
        }

        if (mesInt < 1 || mesInt > 12)
        {
            return false;
        }

        DateTime dataVerificada = new DateTime(anoInt, mesInt, DateTime.DaysInMonth(anoInt, mesInt));
        return dataVerificada > DateTime.UtcNow;
    }
}