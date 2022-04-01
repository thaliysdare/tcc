using System.Collections.Generic;
using System.Net.Http;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class UsuarioService : IUsuarioService
    {
        public readonly HttpClient _tccApi;
        public readonly GenericoService genericoService;

        public UsuarioService(IHttpClientFactory httpClientFactory)
        {
            genericoService = new GenericoService(httpClientFactory);
        }

        public UsuarioRetorno ValidarAutenticacao(UsuarioAutenticacao usuario)
        {
            return genericoService.Inserir<UsuarioRetorno>("usuarios/autenticar", usuario);
        }

        public List<UsuarioRetorno> RecuperarTodos()
        {
            return genericoService.RecuperarTodos<UsuarioRetorno>("usuarios");
        }

        public UsuarioRetorno Recuperar(int id)
        {
            return genericoService.Recuperar<UsuarioRetorno>("usuarios", id);
        }

        public UsuarioRetorno Inserir(UsuarioEnvio usuarioEnvio)
        {
            return genericoService.Inserir<UsuarioRetorno>("usuarios", usuarioEnvio);
        }

        public void Editar(int id, UsuarioEnvio usuarioEnvio)
        {
            genericoService.Editar("usuarios", id, usuarioEnvio);
        }

        public void Excluir(int id)
        {
            genericoService.Excluir("usuarios", id);
        }

    }
}
