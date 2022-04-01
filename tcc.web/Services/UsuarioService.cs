using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using tcc.web.Models.API;
using tcc.web.Services.IService;

namespace tcc.web.Services
{
    public class UsuarioService : IUsuarioService
    {
        public readonly HttpClient _tccApi;

        public UsuarioService(IHttpClientFactory httpClientFactory)
        {
            _tccApi = httpClientFactory.CreateClient("tcc.api");
        }

        public UsuarioRetorno ValidarAutenticacao(UsuarioAutenticacao usuario)
        {
            var jsonEnvio = JsonSerializer.Serialize(usuario);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _tccApi.PostAsync($"usuarios/autenticar", content).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<UsuarioRetorno>(json);
        }

        public UsuarioRetorno Inserir(UsuarioEnvio envio)
        {
            var jsonEnvio = JsonSerializer.Serialize(envio);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _tccApi.PostAsync($"usuarios", content).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<UsuarioRetorno>(json);
        }
        public UsuarioRetorno Editar(UsuarioEnvio envio)
        {
            var jsonEnvio = JsonSerializer.Serialize(envio);
            var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");
            var response = _tccApi.PutAsync($"usuarios/{10}", content).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<UsuarioRetorno>(json);
        }
        public UsuarioRetorno Recuperar(int id)
        {
            var json = _tccApi.GetStringAsync($"usuarios/{id}").Result;
            return JsonSerializer.Deserialize<UsuarioRetorno>(json);
        }
        public List<UsuarioRetorno> RecuperarTodos()
        {
            var json = _tccApi.GetStringAsync($"usuarios").Result;
            return JsonSerializer.Deserialize<List<UsuarioRetorno>>(json);
        }
        public List<UsuarioRetorno> RecuperarApenasAtivos()
        {
            var json = _tccApi.GetStringAsync($"usuarios").Result;
            return JsonSerializer.Deserialize<List<UsuarioRetorno>>(json).Where(x => x.Ativo).ToList();
        }

        public void Excluir(int id)
        {
            var response = _tccApi.DeleteAsync($"usuarios/{id}").Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
