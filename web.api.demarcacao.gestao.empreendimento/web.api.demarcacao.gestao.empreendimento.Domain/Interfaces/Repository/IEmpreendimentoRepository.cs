using web.api.demarcacao.gestao.empreendimento.Domain.Entities;
using web.api.demarcacao.gestao.empreendimento.Domain.Interfaces.Repository.Common;

namespace web.api.demarcacao.gestao.empreendimento.Domain.Interfaces.Repository
{
    public interface IEmpreendimentoRepository : IRepository<Empreendimento, long>
    {

    }
}
