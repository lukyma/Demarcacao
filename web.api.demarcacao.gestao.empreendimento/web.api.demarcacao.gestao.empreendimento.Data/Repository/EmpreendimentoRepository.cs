using web.api.demarcacao.gestao.empreendimento.Data.Context;
using web.api.demarcacao.gestao.empreendimento.Data.Repository.Common;
using web.api.demarcacao.gestao.empreendimento.Domain.Entities;
using web.api.demarcacao.gestao.empreendimento.Domain.Interfaces.Repository;

namespace web.api.demarcacao.gestao.empreendimento.Data.Repository
{
    public class EmpreendimentoRepository<TContext> : Repository<Empreendimento, long, TContext>, IEmpreendimentoRepository
        where TContext : IDemarcacaoPostgressContext

    {
        public EmpreendimentoRepository(TContext dbContext)
            : base(dbContext)
        {

        }
    }
}
