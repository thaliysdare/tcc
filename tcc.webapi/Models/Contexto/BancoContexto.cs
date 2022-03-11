using Microsoft.EntityFrameworkCore;

namespace tcc.webapi.Models.Contexto
{
    public class BancoContexto : DbContext
    {
        public BancoContexto(DbContextOptions<BancoContexto> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseNpgsql("Host=ec2-34-205-46-149.compute-1.amazonaws.com;Port=5432;Database=d1m4s8jddocgp1;Username=qtlhpunflneudo;Password=53acc2a4205585d18ed31f44957c429a3b06dbeec407dd39f8e22736f810dc22;Pooling=true;SSL Mode=Require;TrustServerCertificate=True;");

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Orcamento> Orcamento { get; set; }
        public DbSet<OrdemServico> OrdermServico { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<ServicoOrcamento> ServicoOrcamento { get; set; }
        public DbSet<ServicoOrdemServico> ServicoOrdemServico { get; set; }
        public DbSet<Veiculo> Veiculo { get; set; }
    }
}
