using web.api.demarcacao.gestao.terreno.Data.Context;
using web.api.demarcacao.gestao.terreno.Data.Repository.Common;
using web.api.demarcacao.gestao.terreno.Domain.Entities;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.Repository;

namespace web.api.demarcacao.gestao.terreno.Data.Repository
{
    public class CoordenadaRepository<TContext> : Repository<Coordenada, long, TContext>, ICoordenadaRepository
        where TContext : IDemarcacaoPostgressContext
    {
        public CoordenadaRepository(TContext dbContext)
            : base(dbContext)
        {
        }
    }
}
