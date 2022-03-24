using tcc.webapi.Models;

namespace tcc.webapi.Repositories.IRepositories
{
    public interface IServicoRepository : IGenericoRepository<Servico>
    {
        bool VerificarServicoAtivo(int id);
    }
}
