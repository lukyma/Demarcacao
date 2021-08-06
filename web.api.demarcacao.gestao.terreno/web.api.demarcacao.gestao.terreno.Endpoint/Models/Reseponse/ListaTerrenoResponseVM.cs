using System.Collections.Generic;

namespace web.api.demarcacao.gestao.terreno.Endpoint.Models.Reseponse
{
    public class ListaTerrenoResponseVM : PaginacaoResponseVM
    {
        public IEnumerable<TerrenoResponseVM> Itens { get; set; }
    }
}
