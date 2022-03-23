using System;

namespace tcc.web.Utils
{
    public static class FormataContato
    {
        public static string FormatarContato(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return string.Empty;
            if (valor.Length == 11) return FormatarCelular(valor);
            if (valor.Length == 10) return FormatarTelefone(valor);
            return valor;
        }

        public static string FormatarCelular(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return string.Empty;
            return Convert.ToUInt64(valor).ToString(@"(00)0\.0000\.0000");
        }

        public static string FormatarTelefone(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return string.Empty;
            return Convert.ToUInt64(valor).ToString(@"(00)0000\.0000");
        }

        public static string SemFormatacao(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return string.Empty;
            return valor.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }
    }
}
