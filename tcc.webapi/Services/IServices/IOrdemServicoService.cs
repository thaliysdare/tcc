using System.Collections.Generic;
using tcc.webapi.Models;
using tcc.webapi.Models.DTO;

namespace tcc.webapi.Services.IServices
{
    public interface IOrdemServicoService : IGenericoService
    {
        OrdemServico Inserir(OrdemServico model, List<ServicoOrdemServicoEnvioDTO> listaItens);
        void Editar(int id, OrdemServico model, List<ServicoOrdemServicoEnvioDTO> listaItens);
        void Reiniciar(int id);
        void Paralizar(int id, string motivo);
        void Cancelar(int id);
        void Finalizar(int id);
        void Iniciar(int id);
    }
}
