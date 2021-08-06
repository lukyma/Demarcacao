using System.Collections.Generic;

namespace web.api.demarcacao.gestao.terreno.Service.Application.Strategy
{
    public class TerrenoRequest
    {
        public long IdEmpreendimento { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<CoordenadaRequest> Coordenadas { get; set; }
    }
}
