using System;
using System.Transactions;
using tcc.webapi.Models;
using tcc.webapi.Repositories;
using tcc.webapi.Repositories.IRepositories;
using tcc.webapi.Services.IServices;

namespace tcc.webapi.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public ClienteService(IClienteRepository clienteRepository, IEnderecoRepository enderecoRepository)
        {
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
        }

        public Cliente Inserir(Cliente cliente)
        {
            var clienteNovo = default(Cliente);
            using (var scope = new TransactionScope())
            {
                if (cliente.Endereco != null)
                {
                    var novoEndereco = _enderecoRepository.InserirERecuperar(cliente.Endereco);
                    cliente.EnderecoId = novoEndereco.EnderecoId;
                }

                clienteNovo = _clienteRepository.InserirERecuperar(cliente);
                scope.Complete();
            }
            return clienteNovo;
        }

        public void Editar(Cliente cliente)
        {
            using (var scope = new TransactionScope())
            {
                var originalCliente = _clienteRepository.RecuperarPorId(cliente.ClienteId);
                originalCliente.Nome = cliente.Nome;
                originalCliente.Sobrenome = cliente.Sobrenome;
                originalCliente.CPFOuCNPJ = cliente.CPFOuCNPJ;
                originalCliente.Telefone1 = cliente.Telefone1;
                originalCliente.Telefone2 = cliente.Telefone2;

                if (cliente.EnderecoId.HasValue)
                {
                    var originalEndereco = _enderecoRepository.RecuperarPorId(cliente.EnderecoId.Value);
                    originalEndereco.Rua = cliente.Endereco.Rua;
                    originalEndereco.Numero = cliente.Endereco.Numero;
                    originalEndereco.Complemento = cliente.Endereco.Complemento;
                    originalEndereco.Bairro = cliente.Endereco.Bairro;
                    originalEndereco.Cidade = cliente.Endereco.Cidade;
                    originalEndereco.Estado = cliente.Endereco.Estado;
                }

                scope.Complete();
            }
        }

        public void Excluir(Cliente cliente)
        {
            using (var scope = new TransactionScope())
            {
                if (cliente.EnderecoId.HasValue)
                {
                    var enderecoOriginal = _enderecoRepository.RecuperarPorId(cliente.EnderecoId.Value);
                    _enderecoRepository.Excluir(enderecoOriginal);
                }

                var clienteOriginal = _clienteRepository.RecuperarPorId(cliente.ClienteId);
                _clienteRepository.Excluir(clienteOriginal);

                scope.Complete();
            }
        }
    }
}
