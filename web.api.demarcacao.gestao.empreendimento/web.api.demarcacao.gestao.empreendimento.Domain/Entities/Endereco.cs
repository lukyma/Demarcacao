using System.Collections.Generic;
using web.api.demarcacao.gestao.empreendimento.Domain.Entities.Core;

namespace web.api.demarcacao.gestao.empreendimento.Domain.Entities
{
    public class Endereco : ValueObject
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Referencia { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Logradouro;
            yield return Numero;
            yield return Complemento;
            yield return Bairro;
            yield return Cidade;
            yield return Estado;
            yield return Cep;
        }
    }
}
