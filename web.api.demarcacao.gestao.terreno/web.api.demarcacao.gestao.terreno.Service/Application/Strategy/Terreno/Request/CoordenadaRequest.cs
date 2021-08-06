namespace web.api.demarcacao.gestao.terreno.Service.Application.Strategy
{
    public class CoordenadaRequest
    {
        public long Id { get; set; }
        public int Ordem { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
