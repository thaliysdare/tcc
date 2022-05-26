using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using tcc.webapi.Models;
using tcc.webapi.Models.Contexto;
using tcc.webapi.Repositories;
using Bogus.Extensions.Brazil;
using Bogus;
using tcc.webapi.Repositories.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using tcc.webapi.Services.IServices;
using tcc.webapi.Services;
using System.Linq;

namespace tcc.test
{
    [TestClass]
    public class CargaInicialFake
    {
        private readonly IClienteService _clienteService;

        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IVeiculoRepository _veiculoRepository;

        public CargaInicialFake()
        {
            var service = new ServiceCollection();
            service.AddDbContext<BancoContexto>();

            service.AddTransient<IClienteService, ClienteService>();

            service.AddTransient<IClienteRepository, ClienteRepository>();
            service.AddTransient<IEnderecoRepository, EnderecoRepository>();
            service.AddTransient<IVeiculoRepository, VeiculoRepository>();

            var provider = service.BuildServiceProvider();
            _clienteService = provider.GetService<IClienteService>();

            _clienteRepository = provider.GetService<IClienteRepository>();
            _enderecoRepository = provider.GetService<IEnderecoRepository>();
            _veiculoRepository = provider.GetService<IVeiculoRepository>();
        }

        [TestMethod]
        public void CargaServicos()
        {
        }

        [TestMethod]
        public void CargaClientes()
        {
            int qtd = 10;

            var clienteFake = new Faker<Cliente>("pt_BR")
                .RuleFor(x => x.Nome, y => y.Name.FirstName(Bogus.DataSets.Name.Gender.Female))
                .RuleFor(x => x.Sobrenome, y => y.Name.LastName(Bogus.DataSets.Name.Gender.Female))
                .RuleFor(x => x.CPFOuCNPJ, y => y.Person.Cpf(false))
                .RuleFor(x => x.Telefone1, y => y.Phone.PhoneNumber("##########"));

            var clientes = clienteFake.Generate(qtd);

            var endereoFake = new Faker<Endereco>("pt_BR")
                .RuleFor(x => x.Rua, y => y.Address.StreetName())
                .RuleFor(x => x.Numero, y => y.Address.BuildingNumber())
                .RuleFor(x => x.CEP, y => y.Address.ZipCode())
                .RuleFor(x => x.Bairro, y => y.Address.StreetSuffix())
                .RuleFor(x => x.Cidade, y => y.Address.City())
                .RuleFor(x => x.Estado, y => "ES");

            var enderecos = endereoFake.Generate(qtd);

            var veiculoFake = new Faker<Veiculo>("pt_BR")
                .RuleFor(x => x.Placa, y => ($"{y.Random.String2(1)}{y.Random.String2(1)}{y.Random.String2(1)}{y.Random.Number(1)}{y.Random.String2(1)}{y.Random.Number(1)}{y.Random.Number(1)}").ToUpper())
                .RuleFor(x => x.Marca, y => y.Vehicle.Manufacturer());

            var veiculos = veiculoFake.Generate(qtd);

            var i = 0;
            foreach (var cliente in clientes)
            {
                cliente.Endereco = enderecos[i];
                cliente.Veiculo = new List<Veiculo>
                {
                    veiculos[i]
                };

                //var novoCliente = _clienteService.InserirCliente(cliente);
                //_clienteService.InserirVeiculo(novoCliente.ClienteId, cliente.Veiculo.FirstOrDefault());
                i++;
            }
        }

        [TestMethod]
        public void CargaOS()
        {
        }
    }
}
