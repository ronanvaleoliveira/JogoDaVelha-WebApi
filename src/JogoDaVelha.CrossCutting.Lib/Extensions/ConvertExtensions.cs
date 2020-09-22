using System;
using System.ComponentModel;
using System.Globalization;

namespace JogoDaVelha.CrossCutting.Lib.Extensions
{
    public static class ConvertExtensions
    {
        private static bool IsNull(object value)
        {
            return ((value == null) || (Convert.IsDBNull(value)));
        }

        public static short ToShort(this object value)
        {
            return ToShort(value, 0);
        }

        public static short ToShort(this object value, short valueDefault)
        {
            return IsNull(value) ? valueDefault : Convert.ToInt16(value);
        }

        public static int ToInt32(this object value)
        {
            return ToInt32(value, 0);
        }

        public static int ToInt32(this object value, int valueDefault)
        {
            return IsNull(value) ? valueDefault : Convert.ToInt32(value);
        }

        public static Int16 ToInt16(this object value)
        {
            return ToInt16(value, 0);
        }

        public static Int16 ToInt16(this object value, Int16 valueDefault)
        {
            return IsNull(value) ? valueDefault : Convert.ToInt16(value);
        }

        public static string NullToString(this object value)
        {
            return value == null ? string.Empty : Convert.ToString(value);
        }

        public static string NullToString(this object value, string valueDefault)
        {
            return value == null ? valueDefault : Convert.ToString(value);
        }

        public static decimal ToDecimal(this object value)
        {
            return ToDecimal(value, 0);
        }

        public static decimal ToDecimal(this object value, int valueDefault)
        {
            return IsNull(value) ? valueDefault : Convert.ToDecimal(value);
        }

        public static bool IsTrue(this object value)
        {
            return IsNull(value) ? false : Convert.ToBoolean(value);
        }

        //public static bool IsTrue(this SimNaoEnum value)
        //{
        //    return value == SimNaoEnum.Sim;
        //}

        public static bool IsTrue(this string value)
        {
            return IsNull(value) ? false : value.ToString().Trim().ToUpper() == "T" ? true : false;
        }

        public static bool IsFalse(this object value)
        {
            return IsNull(value) ? true : Convert.ToBoolean(value) == false;
        }

        public static string GetSimNao(this bool? value)
        {
            return GetSimNao(value.IsTrue());
        }

        public static string GetSimNao(this bool value)
        {
            return value ? "Sim" : "Não";
        }

        public static string GetAtivoInativo(this bool value)
        {
            return value ? "Ativo" : "Inativo";
        }

        public static string GetAtivoInativo(this bool? value)
        {
            return GetAtivoInativo(value.IsTrue());
        }

        public static string GetTrueFalse(this bool value)
        {
            return value ? "true" : "false";
        }
        public static string GetTrueFalse(this bool? value)
        {
            return value.IsTrue() ? "true" : "false";
        }

        public static string GetStringBool(this bool value)
        {
            return value ? "T" : "F";
        }

        public static string GetDatePtBR(this DateTime? value)
        {
            return value != null ? value.Value.ToString("dd/MM/yyyy") : string.Empty;
        }

        public static string GetDatePtBR(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy");
        }

        public static string GetDateTimePtBR(this DateTime? value)
        {
            return value != null ? GetDateTimePtBR((DateTime)value) : string.Empty;
        }

        public static string GetDateTimePtBR(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy HH:mm");
        }

        public static string GetDateTimeSecondsPtBR(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string GetDateTimeSecondsPtBR(this DateTime? value)
        {
            return value == null ? string.Empty : GetDateTimeSecondsPtBR((DateTime)value);
        }

        public static string GetTime(this DateTime? value)
        {
            return value != null ? GetTime((DateTime)value) : string.Empty;
        }

        public static string GetTime(this DateTime value)
        {
            return value.ToString("HH:mm");
        }

        public static string GetMoedaPtBR(this decimal? value)
        {
            return value == null ? new Decimal(0).GetMoedaPtBR() : value.Value.GetMoedaPtBR();
        }

        public static string GetMoedaPtBR(this decimal value)
        {
            return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", value);
        }

        public static short GetInt16Bool(this bool value)
        {
            return value ? Convert.ToInt16(1) : Convert.ToInt16(0);
        }

        public static string ToShortDateStringNull(this object value)
        {
            return value == null ? string.Empty : Convert.ToDateTime(value).ToShortDateString();
        }

        public static DateTime? GetDateMaxHour(this DateTime? value)
        {
            if (value != null)
            {
                var valueAux = (DateTime)value;
                value = new DateTime(valueAux.Year, valueAux.Month, valueAux.Day, 23, 59, 59, 999);
            }

            return value;
        }

        public static DateTime? GetDate(this DateTime? value)
        {
            if (value != null)
            {
                value = ((DateTime)value).Date;
            }

            return value;
        }

        public static string ToStringNull(this string value)
        {
            return IsNull(value) ? string.Empty : Convert.ToString(value);
        }

        public static string ToStringNull(this object value)
        {
            return IsNull(value) ? string.Empty : Convert.ToString(value);
        }

        public static string ToStringNull(this object value, string valueDefault)
        {
            return IsNull(value) ? valueDefault : Convert.ToString(value);
        }

        public static string FormatarCpfCnpj(this string value)
        {
            if (value.Trim().Length <= 11)
                return FormatarCpf(value.Trim());
            else
                return FormatarCnpj(value.Trim());
        }
        public static string FormatarCpf(this string value)
        {
            return maskedText(@"000\.000\.000-00", value.Trim());
        }
        public static string FormatarCnpj(this string value)
        {
            return maskedText(@"00\.000\.000/0000-00", value.Trim());
        }
        private static string maskedText(string mask, string value)
        {
            MaskedTextProvider provider = new MaskedTextProvider(mask);
            provider.Set(value);
            return provider.ToString();
        }

        public static string EncodeBase64(this System.Text.Encoding encoding, string text)
        {
            if (text == null)
            {
                return null;
            }

            byte[] textAsBytes = encoding.GetBytes(text);
            return System.Convert.ToBase64String(textAsBytes);
        }

        public static string DecodeBase64(this System.Text.Encoding encoding, string encodedText)
        {
            if (encodedText == null)
            {
                return null;
            }

            byte[] textAsBytes = System.Convert.FromBase64String(encodedText);
            return encoding.GetString(textAsBytes);
        }

        public static bool ToBool(this bool? value)
        {
            return value == null ? false : Convert.ToBoolean(value);
        }
    }
}
