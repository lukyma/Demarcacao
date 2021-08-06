namespace web.api.demarcacao.gestao.terreno.Service.Application.Strategy
{
    public class ExcluiTerrenoRequest : TerrenoQuery
    {
        public ExcluiTerrenoRequest(long id)
        {
            Id = id;
        }
    }
}
