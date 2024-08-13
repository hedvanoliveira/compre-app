using System.ComponentModel;
using System;

namespace CompreApp.Application.Common;

public static class EnumDescricao
{
    static public string Exibir(this Enum enumValue)
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (field == null)
            return enumValue.ToString();
        _ = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
        {
            return attribute.Description;
        }

        return enumValue.ToString();
    }
}
