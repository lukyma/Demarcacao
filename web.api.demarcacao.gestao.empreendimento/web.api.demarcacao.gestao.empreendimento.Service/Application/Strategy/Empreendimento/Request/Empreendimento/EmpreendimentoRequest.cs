using web.api.demarcacao.gestao.empreendimento.Domain.Entities;

namespace web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy
{
    public class EmpreendimentoRequest
    {
        public string Descricao { get; set; }
        public string GrupoEmpresa { get; set; }
        public Endereco Endereco { get; set; }
    }
}
