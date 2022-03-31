using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using tcc.web.Utils;

namespace tcc.web.Controllers
{
    public class GenericoController : Controller
    {
        public JsonRetornoWEB PrepararJsonRetornoErro()
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var retorno = new JsonRetornoWEB
            {
                Sucesso = false,
                Codigo = (int)HttpStatusCode.BadRequest,
                Erros = new List<string>()
            };
            retorno.Erros.AddRange(ModelState.Values.Where(x => x.Errors.Count > 0).Select(x => x.Errors.FirstOrDefault().ErrorMessage).Distinct().ToList());

            return retorno;
        }

        public JsonRetornoWEB PrepararJsonRetorno(GenericoJsonRetorno genericoJsonRetorno, object dados = null, string mensagem = null)
        {
            var json = new JsonRetornoWEB
            {
                Sucesso = true,
                Dados = dados,
                Mensagem = mensagem
            };
            switch (genericoJsonRetorno)
            {
                case GenericoJsonRetorno.GET:
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    break;
                case GenericoJsonRetorno.POST:
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
                    break;
                case GenericoJsonRetorno.PUT:
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    break;
                case GenericoJsonRetorno.DELETE:
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    break;
            }
            return json;
        }

    }

    public enum GenericoJsonRetorno
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
