using Microsoft.EntityFrameworkCore;

namespace tcc.webapi.Models.Contexto
{
    public class BancoContexto : DbContext
    {
        public BancoContexto(DbContextOptions<BancoContexto> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLazyLoadingProxies()
                .UseNpgsql("Host=ec2-34-205-46-149.compute-1.amazonaws.com;Port=5432;Database=d1m4s8jddocgp1;Username=qtlhpunflneudo;Password=53acc2a4205585d18ed31f44957c429a3b06dbeec407dd39f8e22736f810dc22;Pooling=true;SSL Mode=Require;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(x =>
            {
                x.HasIndex(e => e.Login).IsUnique();
                x.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Funcionalidade>(x =>
            {
                x.HasIndex(e => e.Codigo).IsUnique();
            });

            modelBuilder.Entity<Veiculo>(x =>
            {
                x.HasIndex(e => e.Placa).IsUnique();
            });
        }

        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Orcamento> Orcamento { get; set; }
        public virtual DbSet<OrdemServico> OrdermServico { get; set; }
        public virtual DbSet<Servico> Servico { get; set; }
        public virtual DbSet<ServicoOrcamento> ServicoOrcamento { get; set; }
        public virtual DbSet<ServicoOrdemServico> ServicoOrdemServico { get; set; }
        public virtual DbSet<Veiculo> Veiculo { get; set; }
        public virtual DbSet<Funcionalidade> Funcionalidade { get; set; }
        public virtual DbSet<UsuarioFuncionalidade> UsuarioFuncionalidade { get; set; }
    }
}
