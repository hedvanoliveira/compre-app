using System.ComponentModel.DataAnnotations;

namespace CompreApp.Application.Features.ClienteFeature.AutenticarCliente;

public class AutenticarClienteRequest
{
    [Required]
    [MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Length(6, 20)]
    public string Senha { get; set; }
}