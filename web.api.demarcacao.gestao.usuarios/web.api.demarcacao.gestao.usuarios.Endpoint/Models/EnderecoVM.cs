using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace web.api.demarcacao.gestao.usuarios.Endpoint.Models
{
    public class EnderecoVM
    {
        [Required(AllowEmptyStrings = true)]
        public string Logradouro { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Bairro { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Cidade { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Estado { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Cep { get; set; }
    }
}
