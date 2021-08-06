using web.api.demarcacao.gestao.usuarios.Data.Context;
using web.api.demarcacao.gestao.usuarios.Data.Repository.Common;
using web.api.demarcacao.gestao.usuarios.Domain.Entities;
using web.api.demarcacao.gestao.usuarios.Domain.Interfaces.Repository;

namespace web.api.demarcacao.gestao.usuarios.Data.Repository
{
    public class UsuarioRepository<TContext> : Repository<Usuario, long, TContext>, IUsuarioRepository
        where TContext : IDemarcacaoPostgressContext
    {
        public UsuarioRepository(TContext dbContext)
            : base(dbContext)
        {
        }
    }
}
