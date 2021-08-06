using web.api.demarcacao.gestao.terreno.Data.Context;
using web.api.demarcacao.gestao.terreno.Data.Repository.Common;
using web.api.demarcacao.gestao.terreno.Domain.Entities;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.Repository;

namespace web.api.demarcacao.gestao.terreno.Data.Repository
{
    public class TerrenoRepository<TContext> : Repository<Terreno, long, TContext>, ITerrenoRepository
        where TContext : IDemarcacaoPostgressContext
    {
        public TerrenoRepository(TContext dbContext)
            : base(dbContext)
        {
        }
    }
}
