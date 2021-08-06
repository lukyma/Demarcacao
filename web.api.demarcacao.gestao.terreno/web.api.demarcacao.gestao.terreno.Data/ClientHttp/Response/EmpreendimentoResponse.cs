using System.Text.Json.Serialization;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.ClientHttp;

namespace web.api.demarcacao.gestao.terreno.Data.ClientHttp.Response
{
    public class EmpreendimentoResponse : IEmpreendimentoResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }
    }
}
