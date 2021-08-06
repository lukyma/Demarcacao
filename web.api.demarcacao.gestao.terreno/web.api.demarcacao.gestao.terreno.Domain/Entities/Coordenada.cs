using web.api.demarcacao.gestao.terreno.Domain.Entities.Core;

namespace web.api.demarcacao.gestao.terreno.Domain.Entities
{
    public class Coordenada : BaseEntity<long>
    {
        public long IdTerreno { get; set; }
        public int Ordem { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public virtual Terreno Terreno { get; set; }
    }
}
