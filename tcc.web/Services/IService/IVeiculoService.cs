using System.Collections.Generic;
using tcc.web.Models.API;

namespace tcc.web.Services.IService
{
    public interface IVeiculoService
    {
        void Editar(int id, VeiculoEnvio clienteEnvio);
        VeiculoRetorno Inserir(VeiculoEnvio veiculoEnvio);
        VeiculoRetorno Recuperar(int veiculoId);
        List<VeiculoRetorno> RecuperarTodos();
    }
}
