using System.Collections.Generic;

namespace web.api.demarcacao.gestao.terreno.Service.Application.Strategy
{
    public class ListaTerrenoQuery : PaginacaoBase
    {
    }

    public class ListaTerrenoQueryResponse : PaginacaoBase
    {
        public ListaTerrenoQueryResponse(IEnumerable<TerrenoResponse> itens)
        {
            Itens = itens;
        }
        public IEnumerable<TerrenoResponse> Itens { get; }
    }

    public class TerrenoResponse : TerrenoRequest
    {
        public long Id { get; set; }
    }
}
