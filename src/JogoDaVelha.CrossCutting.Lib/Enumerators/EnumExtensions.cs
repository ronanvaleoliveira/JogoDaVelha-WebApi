using System;
using System.ComponentModel;
using System.Reflection;

namespace JogoDaVelha.CrossCutting.Lib.Enumerators
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Retorna o valor informado no atributo Description do enum
        /// </summary>
        public static string GetDescription(this Enum ennum)
        {
            string output = null;
            Type type = ennum.GetType();

            FieldInfo fi = type.GetField(ennum.ToString());
            DescriptionAttribute[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attrs.Length > 0)
            {
                output = attrs[0].Description;
            }

            return output;
        }
    }
}
