using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CompreApp.Application.Common;

public static class JwtBearerService
{
    public static string GenerateToken(string id, string nome, string email, string chaveJwt, DateTime dataExpiracao)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(chaveJwt);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(ClaimTypes.Name, nome),
                new Claim(ClaimTypes.Email, email),
            ]),
            Expires = dataExpiracao,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}