using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tcc.web.Utils
{
    public class JsonRetornoWEB
    {
        public int Codigo { get; set; }
        public bool Sucesso { get; set; } = false;
        public string Mensagem { get; set; }
        public object Dados { get; set; }
        public List<string> Erros { get; set; }
    }
}
