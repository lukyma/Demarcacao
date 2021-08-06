using System.Collections.Generic;

namespace web.api.demarcacao.gestao.usuarios.Endpoint.Models.Reseponse
{
    public class ListaEmpreendimentoResponseVM : PaginacaoResponseVM
    {
        public IEnumerable<EmpreendimentoResponseVM> Itens { get; set; }
    }
}
