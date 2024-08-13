using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CompreApp.Application.Features.PedidoFeature.CadastrarPedido;

public class CadastrarPedidoRequest
{
    [Required]
    public Guid ProdutoId { get; set; }

    public CadastrarPedidoCartaoRequest Cartao { get; set; }
}

public class CadastrarPedidoCartaoRequest
{
    [Required]
    [Length(6, 50)]
    public string NomeProprietarioCartao { get; set; }

    [SwaggerSchema(Description = "Apenas números")]
    [Required]
    [Length(14, 16)]
    public string NumeroCartao { get; set; }

   
    [SwaggerSchema(Description = "Mês valido")]
    [Required]
    [Length(2, 2)]
    public string MesVencimento { get; set; }

    [SwaggerSchema(Description = "Ano valido")]
    [Required]
    [Length(2, 2)]
    public string AnoVencimento { get; set; }

    [Required]
    [Length(3, 4)]
    public string CodigoSeguranca { get; set; }

    [Required]
    [Length(3, 29)]
    public string BandeiraCartao { get; set; }
}