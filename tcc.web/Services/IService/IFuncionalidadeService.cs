using System.Collections.Generic;
using tcc.web.Models.API;

namespace tcc.web.Services.IService
{
    public interface IFuncionalidadeService
    {
        void Editar(int id, FuncionalidadeEnvio clienteEnvio);
        FuncionalidadeRetorno Inserir(FuncionalidadeEnvio funcionalidadeEnvio);
        FuncionalidadeRetorno Recuperar(int veiculoId);
        List<FuncionalidadeRetorno> RecuperarTodos();
    }
}
