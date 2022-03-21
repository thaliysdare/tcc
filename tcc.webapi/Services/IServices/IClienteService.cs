using tcc.webapi.Models;

namespace tcc.webapi.Services.IServices
{
    public interface IClienteService : IGenericoService
    {
        Cliente InserirCliente(Cliente cliente);
        void EditarCliente(int id, Cliente cliente);
        void InativarCliente(int id);
        Veiculo InserirVeiculo(int clienteId, Veiculo veiculo);
        void EditarVeiculo(int clienteId, int veiculoId, Veiculo veiculo);
        void InativarVeiculo(int clienteId, int veiculoId);
    }
}
