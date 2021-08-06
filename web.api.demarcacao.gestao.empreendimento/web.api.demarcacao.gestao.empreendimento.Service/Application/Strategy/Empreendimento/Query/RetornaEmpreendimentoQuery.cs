namespace web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy
{
    public class RetornaEmpreendimentoQuery
    {
        public RetornaEmpreendimentoQuery(long idEmpreendimento)
        {
            IdEmpreendimento = idEmpreendimento;
        }
        public long IdEmpreendimento { get; }
    }

    public class RetornarEmpreendimentoQueryResponse : EmpreendimentoRequest
    {
        public long Id { get; set; }
    }
}
