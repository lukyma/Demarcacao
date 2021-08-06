using Microsoft.EntityFrameworkCore;
using web.api.demarcacao.gestao.empreendimento.Data.Mapping;

namespace web.api.demarcacao.gestao.empreendimento.Data.Context
{
    public class DemarcacaoGestaoEmpreendimentoPostgressContext : DbContext, IDemarcacaoGestaoEmpreendimentoPostgressContext
    {
        public DemarcacaoGestaoEmpreendimentoPostgressContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmpreendimentoConfiguration());
        }
    }
}
