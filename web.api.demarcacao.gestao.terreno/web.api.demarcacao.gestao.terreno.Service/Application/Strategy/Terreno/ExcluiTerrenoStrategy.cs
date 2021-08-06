using patterns.strategy;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.terreno.CrossCutting.Core;
using web.api.demarcacao.gestao.terreno.Domain.Entities;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.Repository;

namespace web.api.demarcacao.gestao.terreno.Service.Application.Strategy
{
    public class ExcluiTerrenoStrategy : IStrategy<ExcluiTerrenoRequest, DefaultResponse>
    {
        private ITerrenoRepository TerrenoRepository { get; }
        private IDemarcacaoUnitOfWork UnitOfWork { get; }
        public ExcluiTerrenoStrategy(ITerrenoRepository terrenoRepository,
                                     IDemarcacaoUnitOfWork unitOfWork)
        {
            TerrenoRepository = terrenoRepository;
            UnitOfWork = unitOfWork;
        }

        public async Task<DefaultResponse> HandleAsync(ExcluiTerrenoRequest request, CancellationToken cancellationToken)
        {
            if (TerrenoRepository.TryGet(request.Id, out Terreno terreno))
            {
                TerrenoRepository.Delete(terreno);
                await UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            return new DefaultResponse();
        }
    }
}
