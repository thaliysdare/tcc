using System.Linq;
using System.Transactions;
using tcc.webapi.Models;
using tcc.webapi.Repositories.IRepositories;
using tcc.webapi.Services.IServices;

namespace tcc.webapi.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IVeiculoRepository _veiculoRepository;

        public ClienteService(IClienteRepository clienteRepository,
                              IEnderecoRepository enderecoRepository,
                              IVeiculoRepository veiculoRepository)
        {
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
            _veiculoRepository = veiculoRepository;
        }

        #region[Cliente]
        public Cliente InserirCliente(Cliente cliente)
        {
            var clienteNovo = default(Cliente);
            using (var scope = new TransactionScope())
            {
                if (cliente.Endereco != null)
                {
                    var novoEndereco = _enderecoRepository.InserirERecuperar(cliente.Endereco);
                    cliente.EnderecoId = novoEndereco.EnderecoId;
                }

                cliente.IdcStatusCliente = Enums.StatusClienteEnum.Ativo;
                clienteNovo = _clienteRepository.InserirERecuperar(cliente);
                scope.Complete();
            }
            return clienteNovo;
        }

        public void EditarCliente(int id, Cliente cliente)
        {
            var originalCliente = _clienteRepository.RecuperarPorId(id);
            VerificarClienteLiberadoParaAlteracao(originalCliente);

            using (var scope = new TransactionScope())
            {
                originalCliente.Nome = cliente.Nome;
                originalCliente.Sobrenome = cliente.Sobrenome;
                originalCliente.CPFOuCNPJ = cliente.CPFOuCNPJ;
                originalCliente.Telefone1 = cliente.Telefone1;
                originalCliente.Telefone2 = cliente.Telefone2;

                if (cliente.Endereco != null)
                {
                    var originalEndereco = _enderecoRepository.RecuperarPorId(originalCliente.EnderecoId.Value);
                    originalEndereco.Rua = cliente.Endereco.Rua;
                    originalEndereco.Numero = cliente.Endereco.Numero;
                    originalEndereco.Complemento = cliente.Endereco.Complemento;
                    originalEndereco.Bairro = cliente.Endereco.Bairro;
                    originalEndereco.Cidade = cliente.Endereco.Cidade;
                    originalEndereco.Estado = cliente.Endereco.Estado;

                    _enderecoRepository.Editar(originalEndereco);
                }

                _clienteRepository.Editar(originalCliente);
                scope.Complete();
            }
        }

        public void InativarCliente(int id)
        {
            using (var scope = new TransactionScope())
            {
                var clienteOriginal = _clienteRepository.RecuperarPorId(id);
                if (clienteOriginal.EnderecoId.HasValue)
                {
                    var enderecoOriginal = _enderecoRepository.RecuperarPorId(clienteOriginal.EnderecoId.Value);
                    _enderecoRepository.Excluir(enderecoOriginal);
                }

                if (clienteOriginal.Veiculo.Any())
                {
                    foreach (var veiculo in clienteOriginal.Veiculo)
                    {
                        veiculo.IdcStatusVeiculo = Enums.StatusVeiculoEnum.Inativo;
                        _veiculoRepository.Editar(veiculo);
                    }
                }

                clienteOriginal.IdcStatusCliente = Enums.StatusClienteEnum.Inativo;
                _clienteRepository.Editar(clienteOriginal);

                scope.Complete();
            }
        }
        #endregion

        #region[Veiculo]
        public Veiculo InserirVeiculo(int clienteId, Veiculo veiculo)
        {
            var veiculoNovo = default(Veiculo);
            using (var scope = new TransactionScope())
            {
                var originalCliente = _clienteRepository.RecuperarPorId(clienteId);
                veiculo.ClienteId = originalCliente.ClienteId;
                veiculoNovo = _veiculoRepository.InserirERecuperar(veiculo);

                scope.Complete();
            }
            return veiculoNovo;
        }

        public void EditarVeiculo(int clienteId, int veiculoId, Veiculo veiculo)
        {
            var originalCliente = _clienteRepository.RecuperarPorId(clienteId);
            VerificarClienteLiberadoParaAlteracao(originalCliente);

            var originalVeiculo = _veiculoRepository.RecuperarPorId(veiculoId);
            VerificarVeiculoLiberadoParaAlteracao(originalVeiculo);

            using (var scope = new TransactionScope())
            {
                originalVeiculo.Marca = veiculo.Marca;
                originalVeiculo.Placa = veiculo.Placa;
                _veiculoRepository.Editar(originalVeiculo);

                scope.Complete();
            }
        }

        public void InativarVeiculo(int clienteId, int veiculoId)
        {
            var originalVeiculo = _veiculoRepository.RecuperarPorId(veiculoId);
            if (originalVeiculo == null) throw new System.Exception("Veiculo não encontrado");
            if (originalVeiculo.ClienteId != clienteId) throw new System.Exception("Veiculo não pertence a esse cliente");

            using (var scope = new TransactionScope())
            {
                originalVeiculo.IdcStatusVeiculo = Enums.StatusVeiculoEnum.Inativo;
                _veiculoRepository.Editar(originalVeiculo);
                scope.Complete();
            }
        }
        #endregion

        #region[Metodos auxiliares]
        private void VerificarClienteLiberadoParaAlteracao(Cliente cliente)
        {
            if (cliente == null)
                throw new System.Exception("Cliente não encontrado");
            if (cliente.IdcStatusCliente == Enums.StatusClienteEnum.Inativo)
                throw new System.Exception("Cliente inativo não pode ser alterado");
        }
        private void VerificarVeiculoLiberadoParaAlteracao(Veiculo veiculo)
        {
            if (veiculo == null)
                throw new System.Exception("Veiculo não encontrado");

            if (veiculo.IdcStatusVeiculo == Enums.StatusVeiculoEnum.Inativo)
                throw new System.Exception("Veiculo inativo não pode ser alterado");
        }
        #endregion
    }
}
