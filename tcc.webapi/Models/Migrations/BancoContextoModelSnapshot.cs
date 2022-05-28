﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using tcc.webapi.Models.Contexto;

namespace tcc.webapi.Migrations
{
    [DbContext(typeof(BancoContexto))]
    partial class BancoContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("tcc.webapi.Models.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CPFOuCNPJ")
                        .HasColumnType("text");

                    b.Property<int?>("EnderecoId")
                        .HasColumnType("integer");

                    b.Property<int>("IdcStatusCliente")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sobrenome")
                        .HasColumnType("text");

                    b.Property<string>("Telefone1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefone2")
                        .HasColumnType("text");

                    b.HasKey("ClienteId");

                    b.HasIndex("EnderecoId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("tcc.webapi.Models.Endereco", b =>
                {
                    b.Property<int>("EnderecoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Complemento")
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("EnderecoId");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("tcc.webapi.Models.Funcionalidade", b =>
                {
                    b.Property<int>("FuncionalidadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Nivel")
                        .HasColumnType("integer");

                    b.HasKey("FuncionalidadeId");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("Funcionalidade");
                });

            modelBuilder.Entity("tcc.webapi.Models.Orcamento", b =>
                {
                    b.Property<int>("OrcamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataOrcamento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataPrevisao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("IdcStatusOrcamento")
                        .HasColumnType("integer");

                    b.Property<string>("Observacao")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int?>("OrdemServicoId")
                        .HasColumnType("integer");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.Property<double>("ValorOrcamento")
                        .HasColumnType("double precision");

                    b.Property<int>("VeiculoId")
                        .HasColumnType("integer");

                    b.HasKey("OrcamentoId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("UsuarioId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("Orcamento");
                });

            modelBuilder.Entity("tcc.webapi.Models.OrdemServico", b =>
                {
                    b.Property<int>("OrdemServicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DataPrevisao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DataSaida")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("IdcStatusOrdemServico")
                        .HasColumnType("integer");

                    b.Property<int>("KMAtual")
                        .HasColumnType("integer");

                    b.Property<string>("Observacao")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.Property<double>("ValorOrdemServico")
                        .HasColumnType("double precision");

                    b.Property<int>("VeiculoId")
                        .HasColumnType("integer");

                    b.HasKey("OrdemServicoId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("UsuarioId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("OrdemServico");
                });

            modelBuilder.Entity("tcc.webapi.Models.Servico", b =>
                {
                    b.Property<int>("ServicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DescricaoResumida")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IdcStatusServico")
                        .HasColumnType("integer");

                    b.Property<double>("Valor")
                        .HasColumnType("double precision");

                    b.HasKey("ServicoId");

                    b.ToTable("Servico");
                });

            modelBuilder.Entity("tcc.webapi.Models.ServicoOrcamento", b =>
                {
                    b.Property<int>("ServicoOrcamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("OrcamentoId")
                        .HasColumnType("integer");

                    b.Property<int>("ServicoId")
                        .HasColumnType("integer");

                    b.Property<double>("Valor")
                        .HasColumnType("double precision");

                    b.HasKey("ServicoOrcamentoId");

                    b.HasIndex("OrcamentoId");

                    b.HasIndex("ServicoId");

                    b.ToTable("ServicoOrcamento");
                });

            modelBuilder.Entity("tcc.webapi.Models.ServicoOrdemServico", b =>
                {
                    b.Property<int>("ServicoOrdemServicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("OrdemServicoId")
                        .HasColumnType("integer");

                    b.Property<int>("ServicoId")
                        .HasColumnType("integer");

                    b.Property<double>("Valor")
                        .HasColumnType("double precision");

                    b.HasKey("ServicoOrdemServicoId");

                    b.HasIndex("OrdemServicoId");

                    b.HasIndex("ServicoId");

                    b.ToTable("ServicoOrdemServico");
                });

            modelBuilder.Entity("tcc.webapi.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IdcStatusUsuario")
                        .HasColumnType("integer");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UsuarioId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("tcc.webapi.Models.UsuarioFuncionalidade", b =>
                {
                    b.Property<int>("UsuarioFuncionalidadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FuncionalidadeId")
                        .HasColumnType("integer");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.HasKey("UsuarioFuncionalidadeId");

                    b.HasIndex("FuncionalidadeId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioFuncionalidade");
                });

            modelBuilder.Entity("tcc.webapi.Models.Veiculo", b =>
                {
                    b.Property<int>("VeiculoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ClienteId")
                        .HasColumnType("integer");

                    b.Property<int>("IdcStatusVeiculo")
                        .HasColumnType("integer");

                    b.Property<string>("Marca")
                        .HasColumnType("text");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("VeiculoId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Veiculo");
                });

            modelBuilder.Entity("tcc.webapi.Models.Cliente", b =>
                {
                    b.HasOne("tcc.webapi.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId");

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("tcc.webapi.Models.Orcamento", b =>
                {
                    b.HasOne("tcc.webapi.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tcc.webapi.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tcc.webapi.Models.Veiculo", "Veiculo")
                        .WithMany()
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Usuario");

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("tcc.webapi.Models.OrdemServico", b =>
                {
                    b.HasOne("tcc.webapi.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tcc.webapi.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tcc.webapi.Models.Veiculo", "Veiculo")
                        .WithMany()
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Usuario");

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("tcc.webapi.Models.ServicoOrcamento", b =>
                {
                    b.HasOne("tcc.webapi.Models.Orcamento", "Orcamento")
                        .WithMany("ServicoOrcamento")
                        .HasForeignKey("OrcamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tcc.webapi.Models.Servico", "Servico")
                        .WithMany("ServicoOrcamento")
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orcamento");

                    b.Navigation("Servico");
                });

            modelBuilder.Entity("tcc.webapi.Models.ServicoOrdemServico", b =>
                {
                    b.HasOne("tcc.webapi.Models.OrdemServico", "OrdemServico")
                        .WithMany("ServicoOrdemServico")
                        .HasForeignKey("OrdemServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tcc.webapi.Models.Servico", "Servico")
                        .WithMany("ServicoOrdemServico")
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrdemServico");

                    b.Navigation("Servico");
                });

            modelBuilder.Entity("tcc.webapi.Models.UsuarioFuncionalidade", b =>
                {
                    b.HasOne("tcc.webapi.Models.Funcionalidade", "Funcionalidade")
                        .WithMany()
                        .HasForeignKey("FuncionalidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tcc.webapi.Models.Usuario", "Usuario")
                        .WithMany("UsuarioFuncionalidade")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionalidade");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("tcc.webapi.Models.Veiculo", b =>
                {
                    b.HasOne("tcc.webapi.Models.Cliente", "Cliente")
                        .WithMany("Veiculo")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("tcc.webapi.Models.Cliente", b =>
                {
                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("tcc.webapi.Models.Orcamento", b =>
                {
                    b.Navigation("ServicoOrcamento");
                });

            modelBuilder.Entity("tcc.webapi.Models.OrdemServico", b =>
                {
                    b.Navigation("ServicoOrdemServico");
                });

            modelBuilder.Entity("tcc.webapi.Models.Servico", b =>
                {
                    b.Navigation("ServicoOrcamento");

                    b.Navigation("ServicoOrdemServico");
                });

            modelBuilder.Entity("tcc.webapi.Models.Usuario", b =>
                {
                    b.Navigation("UsuarioFuncionalidade");
                });
#pragma warning restore 612, 618
        }
    }
}
