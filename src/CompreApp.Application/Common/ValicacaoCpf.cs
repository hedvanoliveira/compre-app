using System;
using System.Collections.Generic;
using System.Linq;

namespace CompreApp.Application.Common;

public static class ValicacaoCpf
{
    public static bool CpfValido(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        // Remove qualquer caracter não numérico
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return false;

        // Verifica se todos os dígitos são iguais, como "11111111111"
        if (cpf.Distinct().Count() == 1)
            return false;

        var multiplicadores1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicadores2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCpf = cpf.Substring(0, 9);
        var soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

        var resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        var digito = resto.ToString();

        tempCpf += digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;
        digito += resto.ToString();

        return cpf.EndsWith(digito);
    }
}