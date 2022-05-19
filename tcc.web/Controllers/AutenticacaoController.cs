using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using tcc.web.Models;
using tcc.web.Services.IService;
using tcc.web.Utils;

namespace tcc.web.Controllers
{
    // https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/cookie?view=aspnetcore-6.0

    [Route("[controller]")]
    [Authorize]
    public class AutenticacaoController : GenericoController
    {
        public AutenticacaoController(IUsuarioService usuarioService) : base(usuarioService) { }

        [AllowAnonymous]
        [Route("entrar")]
        [HttpGet]
        public IActionResult CarregarLogar()
        {
            return View("Logar", new AutenticacaoViewModel());
        }

        [AllowAnonymous]
        [Route("entrar")]
        [HttpPost]
        public async Task<IActionResult> LogarAsync(AutenticacaoViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Logar", viewModel);

            try
            {
                var usuario = _usuarioService.ValidarAutenticacao(viewModel.MapearModel());
                if (usuario == null) throw new Exception("Usuario não encontrado, por favor verifique a senha");
                if (!usuario.Ativo) throw new Exception("Usuario não está mais ativo");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Email),
                    new Claim(ClaimTypes.Sid, usuario.UsuarioId.ToString()),
                    new Claim("FullName", usuario.NomeCompleto),
                };

                if (usuario.ListaFuncionalidade == null || !usuario.ListaFuncionalidade.Any()) throw new Exception("Nenhuma permissão atribuida ao usuário");

                foreach (var item in usuario.ListaFuncionalidade)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(claimsIdentity),
                                              authProperties);

                return RedirectToAction("index", "ordemservico");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(viewModel.Login), e.Message);
                ModelState.AddModelError(nameof(viewModel.Senha), e.Message);

                return View("Logar", viewModel);
            }
        }

        [Route("sair")]
        [HttpGet]
        public async Task<IActionResult> LogoffAsync()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("entrar", "autenticacao");
        }

        [Route("acessonegado")]
        [HttpGet]
        public IActionResult AcessoNegado()
        {
            return View();
        }
    }
}
