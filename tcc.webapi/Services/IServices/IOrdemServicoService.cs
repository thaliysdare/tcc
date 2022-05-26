using System.Collections.Generic;
using tcc.webapi.Models;
using tcc.webapi.Models.DTO;

namespace tcc.webapi.Services.IServices
{
    public interface IOrdemServicoService : IGenericoService
    {
        OrdemServico Inserir(OrdemServico model, List<ServicoOrdemServicoEnvioDTO> listaItens);
        void Editar(int id, OrdemServico model, List<ServicoOrdemServicoEnvioDTO> listaItens);
    }
}
