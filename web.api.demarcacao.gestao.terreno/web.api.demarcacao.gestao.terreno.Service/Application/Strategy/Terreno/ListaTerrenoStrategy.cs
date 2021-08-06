using AutoMapper;
using patterns.strategy;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.Repository;

namespace web.api.demarcacao.gestao.terreno.Service.Application.Strategy
{
    public class ListaTerrenoStrategy : IStrategy<ListaTerrenoQuery, ListaTerrenoQueryResponse>
    {
        private IMapper Mapper { get; }
        private ITerrenoRepository TerrenoRepository { get; }
        public ListaTerrenoStrategy(IMapper mapper,
                                    ITerrenoRepository terrenoRepository)
        {
            Mapper = mapper;
            TerrenoRepository = terrenoRepository;
        }

        public async Task<ListaTerrenoQueryResponse> HandleAsync(ListaTerrenoQuery request,
                                                                 CancellationToken cancellationToken)
        {
            var itens = TerrenoRepository.All(request.Pagina, 10, false).Include(o => o.Coordenadas).AsNoTracking().ToList();
            var response = new ListaTerrenoQueryResponse(Mapper.Map<IEnumerable<TerrenoResponse>>(itens.ToList()));
            response.Pagina = request.Pagina + 1;
            return await Task.FromResult(response);
        }
    }
}
