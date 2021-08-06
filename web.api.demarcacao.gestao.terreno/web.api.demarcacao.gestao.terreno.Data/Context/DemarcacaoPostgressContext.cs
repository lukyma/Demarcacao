using Microsoft.EntityFrameworkCore;
using web.api.demarcacao.gestao.terreno.Data.Mapping;

namespace web.api.demarcacao.gestao.terreno.Data.Context
{

    public class DemarcacaoGestaoTerrenoPostgressContext : DbContext, IDemarcacaoGestaoTerrenoPostgressContext
    {
        public DemarcacaoGestaoTerrenoPostgressContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TerrenoConfiguration());
            modelBuilder.ApplyConfiguration(new CoordenadaConfiguration());
        }
    }
}
