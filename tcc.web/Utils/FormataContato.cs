using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tcc.web.Utils
{
    public static class FormataContato
    {
        public static string FormatarContato(this string valor)
        {
            if (valor.Length == 11) return FormatarCelular(valor);
            return FormatarTelefone(valor);
        }

        public static string FormatarCelular(this string valor)
        {
            return Convert.ToUInt64(valor).ToString(@"(00)0\.0000\.0000");
        }

        public static string FormatarTelefone(this string valor)
        {
            return Convert.ToUInt64(valor).ToString(@"(00)0000\.0000");
        }
       
        public static string SemFormatacao(this string valor)
        {
            return valor.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }
    }
}
