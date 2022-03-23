using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tcc.web.Utils
{
    public class TccWebExecption : Exception
    {
        public int StatusCode { get; set; }
        public TccWebExecption(string message, int statusCode) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
