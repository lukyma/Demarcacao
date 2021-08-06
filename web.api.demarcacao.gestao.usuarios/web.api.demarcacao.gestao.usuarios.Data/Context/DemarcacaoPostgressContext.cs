using Microsoft.EntityFrameworkCore;
using web.api.demarcacao.gestao.usuarios.Data.Mapping;

namespace web.api.demarcacao.gestao.usuarios.Data.Context
{
    public class DemarcacaoGestaoUsuarioPostgressContext : DbContext, IDemarcacaoGestaoUsuarioPostgressContext
    {
        public DemarcacaoGestaoUsuarioPostgressContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new InterfaceConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioInterfaceConfiguration());
        }
    }
}
