using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace web.api.demarcacao.gestao.empreendimento.Endpoint.Models
{
    public class EmpreendimentoVM
    {
        [Required(AllowEmptyStrings = true)]
        public string Descricao { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string GrupoEmpresa { get; set; }
        [Required(AllowEmptyStrings = true)]
        public EnderecoVM Endereco { get; set; }
    }

    public class EmpreendimentoResponseVM : EmpreendimentoVM
    {
        public long Id { get; set; }
    }
}
