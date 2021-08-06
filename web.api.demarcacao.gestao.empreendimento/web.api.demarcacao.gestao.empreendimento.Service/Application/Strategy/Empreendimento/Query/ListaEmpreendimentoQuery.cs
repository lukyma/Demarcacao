using System.Collections.Generic;

namespace web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy
{
    public class ListaEmpreendimentoQuery : PaginacaoBase
    {
    }

    public class ListaEmpreendimentoQueryResponse : PaginacaoBase
    {
        public ListaEmpreendimentoQueryResponse(IEnumerable<EmpreendimentoResponse> itens)
        {
            Itens = itens;
        }
        public IEnumerable<EmpreendimentoResponse> Itens { get; }
    }

    public class EmpreendimentoResponse : EmpreendimentoRequest
    {
        public long Id { get; set; }
    }
}
