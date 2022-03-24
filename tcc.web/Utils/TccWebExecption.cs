using System;

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
