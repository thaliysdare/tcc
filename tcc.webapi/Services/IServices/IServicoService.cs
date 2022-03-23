using tcc.webapi.Models;

namespace tcc.webapi.Services.IServices
{
    public interface IServicoService : IGenericoService
    {
        Servico Inserir(Servico model);
        void Editar(int id, Servico model);
        void Inativar(int id);
    }
}
