using System.Collections.Generic;
using tcc.web.Models.API;

namespace tcc.web.Services.IService
{
    public interface IOrdemServicoService
    {
        OrdemServicoRetorno Editar(OrdemServicoEnvio envio);
        OrdemServicoRetorno Inserir(OrdemServicoEnvio envio);
        OrdemServicoRetorno Recuperar(int id);
        void Excluir(int id);
        List<OrdemServicoRetorno> RecuperarTodos();
    }
}
