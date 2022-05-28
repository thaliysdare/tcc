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
        private readonly IServicoRepository _servicoRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IFuncionalidadeRepository _funcionalidadeRepository;
        private readonly IServicoService _servicoService;

        public CargaInicialFake()
        {
            var service = new ServiceCollection();
            service.AddDbContext<BancoContexto>();

            service.AddTransient<IClienteService, ClienteService>();
            service.AddTransient<IUsuarioService, UsuarioService>();
            service.AddTransient<IServicoService, ServicoService>();

            service.AddTransient<IClienteRepository, ClienteRepository>();
            service.AddTransient<IEnderecoRepository, EnderecoRepository>();
            service.AddTransient<IVeiculoRepository, VeiculoRepository>();
            service.AddTransient<IServicoRepository, ServicoRepository>();
            service.AddTransient<IFuncionalidadeRepository, FuncionalidadeRepository>();
            service.AddTransient<IUsuarioRepository, UsuarioRepository>();
            service.AddTransient<IUsuarioFuncionalidadeRepository, UsuarioFuncionalidadeRepository>();

            var provider = service.BuildServiceProvider();

            _clienteService = provider.GetService<IClienteService>();
            _usuarioService = provider.GetService<IUsuarioService>();
            _servicoService = provider.GetService<IServicoService>();

            _clienteRepository = provider.GetService<IClienteRepository>();
            _enderecoRepository = provider.GetService<IEnderecoRepository>();
            _veiculoRepository = provider.GetService<IVeiculoRepository>();
            _servicoRepository = provider.GetService<IServicoRepository>();
            _funcionalidadeRepository = provider.GetService<IFuncionalidadeRepository>();
        }

        [TestMethod]
        public void CargaFuncionalidades()
        {
            var listaFuncionalidade = new List<Funcionalidade>()
            {
                new Funcionalidade(){Codigo = "NV1", Descricao = "Ler e escrever OS/Orçamento.", Nivel = 1},
                new Funcionalidade(){Codigo = "NV2", Descricao = "Ler, criar e editar clientes, serviços, OS e orçamentos", Nivel = 2},
                new Funcionalidade(){Codigo = "NV3", Descricao = "Inativar clientes, serviços, orçamentos e OS.", Nivel = 3},
                new Funcionalidade(){Codigo = "NV4", Descricao = "Relatórios gerenciais.", Nivel = 4},
            };

            foreach (var item in listaFuncionalidade)
            {
                _funcionalidadeRepository.Inserir(item);
            }
        }

        [TestMethod]
        public void CargaUsuarios()
        {
            var listaUsuarios = new List<Usuario>()
            {
                new Usuario()
                {
                    Nome = "Administrador",
                    Sobrenome = "Sistema",
                    Email = "admin@oficina.com.br",
                    Login = "admin",
                    Senha = "1234",
                    UsuarioFuncionalidade = new List<UsuarioFuncionalidade>()
                    {
                        new UsuarioFuncionalidade(){FuncionalidadeId = 1},
                        new UsuarioFuncionalidade(){FuncionalidadeId = 2},
                        new UsuarioFuncionalidade(){FuncionalidadeId = 3},
                        new UsuarioFuncionalidade(){FuncionalidadeId = 4}
                    }
                },
                new Usuario()
                {
                    Nome = "Operador",
                    Sobrenome = "Nivel 1",
                    Email = "ope1@oficina.com.br",
                    Login = "operador1",
                    Senha = "1234",
                    UsuarioFuncionalidade = new List<UsuarioFuncionalidade>()
                    {
                        new UsuarioFuncionalidade(){FuncionalidadeId = 1},
                    }
                },
                new Usuario()
                {
                    Nome = "Operador",
                    Sobrenome = "Nivel 2",
                    Email = "ope2@oficina.com.br",
                    Login = "operador2",
                    Senha = "1234",
                    UsuarioFuncionalidade = new List<UsuarioFuncionalidade>()
                    {
                        new UsuarioFuncionalidade(){FuncionalidadeId = 1},
                        new UsuarioFuncionalidade(){FuncionalidadeId = 2},
                    }
                },
                new Usuario()
                {
                    Nome = "Operador",
                    Sobrenome = "Nivel 3",
                    Email = "ope3@oficina.com.br",
                    Login = "operador3",
                    Senha = "1234",
                    UsuarioFuncionalidade = new List<UsuarioFuncionalidade>()
                    {
                        new UsuarioFuncionalidade(){FuncionalidadeId = 1},
                        new UsuarioFuncionalidade(){FuncionalidadeId = 2},
                        new UsuarioFuncionalidade(){FuncionalidadeId = 3},
                    }
                },
            };

            foreach (var item in listaUsuarios)
            {
                _usuarioService.Inserir(item);
            }
        }

        [TestMethod]
        public void CargaServicos()
        {
            var listaServicos = new List<Servico>()
            {
                new Servico()
                {
                    Descricao = "Troca de óleo e filtro de óleo",
                    DescricaoResumida = "Troca de óleo e filtro",
                    Valor = 30
                },
                new Servico()
                {
                    Descricao = "Troca de pastilha de freio",
                    DescricaoResumida = "Troca de pastilha",
                    Valor = 40
                },
                new Servico()
                {
                    Descricao = "Troca de disco de freio",
                    DescricaoResumida = "Troca de disco",
                    Valor = 70
                },
                new Servico()
                {
                    Descricao = "Troca de amortecedor dianteiro(par)",
                    DescricaoResumida = "Troca de amortecedor dianteiro",
                    Valor = 140
                },
                new Servico()
                {
                    Descricao = "Troca de amortecedor traseiro(par)",
                    DescricaoResumida = "Troca de amortecedor traseiro",
                    Valor = 100
                },
                new Servico()
                {
                    Descricao = "Troca de vela e cabo de ignição",
                    DescricaoResumida = "Troca de vela e cabo",
                    Valor = 70
                },
                new Servico()
                {
                    Descricao = "Troca de embreagem",
                    DescricaoResumida = "Troca de embreagem",
                    Valor = 210
                },
                new Servico()
                {
                    Descricao = "Rastreamento com scanner",
                    DescricaoResumida = "Rastreamento com scanner",
                    Valor = 80
                },
            };

            foreach (var item in listaServicos)
            {
                _servicoService.Inserir(item);
            }
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

                var novoCliente = _clienteService.InserirCliente(cliente);
                _clienteService.InserirVeiculo(novoCliente.ClienteId, cliente.Veiculo.FirstOrDefault());
                i++;
            }
        }

        [TestMethod]
        public void CargaOS()
        {
        }

        [TestMethod]
        public void AlteracaoCarga()
        {
            var listaServicos = _servicoRepository.RecuperarTodos().ToList();
            foreach (var servico in listaServicos)
            {
                servico.IdcStatusServico = webapi.Enums.StatusServicoEnum.Ativo;
                _servicoRepository.Editar(servico);
            }
        }

    }
}
