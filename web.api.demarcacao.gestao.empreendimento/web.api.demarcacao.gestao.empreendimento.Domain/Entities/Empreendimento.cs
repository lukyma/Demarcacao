using web.api.demarcacao.gestao.empreendimento.Domain.Entities.Core;

namespace web.api.demarcacao.gestao.empreendimento.Domain.Entities
{
    public class Empreendimento : AggregateRoot<long>
    {
        public string Descricao { get; set; }
        public string GrupoEmpresa { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
