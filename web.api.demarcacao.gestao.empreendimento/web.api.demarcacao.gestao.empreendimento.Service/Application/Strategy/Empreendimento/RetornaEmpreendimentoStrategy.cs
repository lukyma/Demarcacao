using AutoMapper;
using patterns.strategy;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.empreendimento.Domain.Interfaces.Repository;
using web.api.demarcacao.gestao.empreendimento.Service.Application.Validators;

namespace web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy
{
    public class RetornaEmpreendimentoStrategy : IStrategy<RetornaEmpreendimentoQuery, RetornarEmpreendimentoQueryResponse>
    {
        private IMapper Mapper { get; }
        private IEmpreendimentoRepository EmpreendimentoRepository { get; }
        public RetornaEmpreendimentoStrategy(IMapper mapper,
                                              IEmpreendimentoRepository empreendimentoRepository)
        {
            Mapper = mapper;
            EmpreendimentoRepository = empreendimentoRepository;
        }

        [Validator(typeof(RetornaEmpreendimentoQueryValidator))]
        public async Task<RetornarEmpreendimentoQueryResponse> HandleAsync(RetornaEmpreendimentoQuery request, CancellationToken cancellationToken)
        {
            return Mapper.Map<RetornarEmpreendimentoQueryResponse>(await EmpreendimentoRepository.GetAsync(request.IdEmpreendimento, cancellationToken));
        }
    }
}
