
namespace web.api.demarcacao.gestao.terreno.Service.Application.Strategy
{
    public class RetornaTerrenoQuery : TerrenoQuery
    {
        public RetornaTerrenoQuery(long id)
        {
            Id = id;
        }
    }

    public class RetornaTerrenoQueryResponse : TerrenoRequest
    {
        public (double valor, string valorFormatado) SomaDistanciaPontos { get; set; }
        public (double valor, string valorFormatado) AreaTotal { get; set; }
    }
}
