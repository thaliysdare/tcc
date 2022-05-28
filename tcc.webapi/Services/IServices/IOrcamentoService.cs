using System.Collections.Generic;
using tcc.webapi.Models;
using tcc.webapi.Models.DTO;

namespace tcc.webapi.Services.IServices
{
    public interface IOrcamentoService : IGenericoService
    {
        Orcamento Inserir(Orcamento model, List<ServicoOrcamentoEnvioDTO> listaItens);
        void Editar(int id, Orcamento model, List<ServicoOrcamentoEnvioDTO> listaItens);
    }
}
