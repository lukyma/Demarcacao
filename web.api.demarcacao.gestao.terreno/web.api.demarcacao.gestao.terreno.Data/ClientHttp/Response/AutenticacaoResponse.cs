using System.Text.Json.Serialization;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.ClientHttp.Response;

namespace web.api.demarcacao.gestao.terreno.Data.ClientHttp.Response
{
    public class AutenticacaoResponse : IAutenticacaoResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}
