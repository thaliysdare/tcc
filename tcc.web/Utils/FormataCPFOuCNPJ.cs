using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tcc.web.Utils
{
    public static class FormataCPFOuCNPJ
    {
        public static string FormatarCPFOuCNPJ(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return string.Empty;
            if (valor.Length == 11) return FormatarCPF(valor);
            return FormatarCNPJ(valor);
        }

        public static string FormatarCNPJ(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return string.Empty;
            return Convert.ToUInt64(valor).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string FormatarCPF(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return string.Empty;
            return Convert.ToUInt64(valor).ToString(@"000\.000\.000\-00");
        }
       
        public static string SemFormatacao(this string valor)
        {
            if (string.IsNullOrEmpty(valor)) return string.Empty;
            return valor.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }
    }
}
