using AutoMapper;
using patterns.strategy;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.Repository;

namespace web.api.demarcacao.gestao.terreno.Service.Application.Strategy
{
    public class RetornaTerrenoStrategy : IStrategy<RetornaTerrenoQuery, RetornaTerrenoQueryResponse>
    {
        private IMapper Mapper { get; }
        private ITerrenoRepository TerrenoRepository { get; }
        private ICoordenadaRepository CoordenadaRepository { get; }
        public RetornaTerrenoStrategy(IMapper mapper,
                                      ITerrenoRepository terrenoRepository,
                                      ICoordenadaRepository coordenadaRepository)
        {
            Mapper = mapper;
            TerrenoRepository = terrenoRepository;
            CoordenadaRepository = coordenadaRepository;
        }
        public async Task<RetornaTerrenoQueryResponse> HandleAsync(RetornaTerrenoQuery request, CancellationToken cancellationToken)
        {
            var terreno = await TerrenoRepository.GetAsync(request.Id, cancellationToken);
            if (terreno == null)
            {
                return default;
            }
            terreno.Coordenadas = CoordenadaRepository.Find(o => o.IdTerreno == request.Id).ToList();
            var response = Mapper.Map<RetornaTerrenoQueryResponse>(await TerrenoRepository.GetAsync(request.Id, cancellationToken));
            return response;
        }
    }
}
