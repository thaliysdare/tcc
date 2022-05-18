using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using tcc.web.Models.API;
using tcc.web.Services.IService;
using tcc.web.Utils;

namespace tcc.web.Controllers
{
    public class GenericoController : Controller
    {
        public readonly IUsuarioService _usuarioService;

        public GenericoController(IUsuarioService usuarioService) => _usuarioService = usuarioService;

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

        public UsuarioRetorno RecuperarUsuarioLogado()
        {
            try
            {
                var usuario = HttpContext.Session.Get<UsuarioRetorno>("Usuario");
                if (usuario != null) return usuario;

                var sid = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
                usuario = _usuarioService.Recuperar(Convert.ToInt32(sid.Value));
                HttpContext.Session.Set("Usuario", usuario);
                return usuario;
            }
            catch (Exception e)
            {
                RedirectToAction("sair", "autenticacao");
            }
            return null;
        }

        public enum GenericoJsonRetorno
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}