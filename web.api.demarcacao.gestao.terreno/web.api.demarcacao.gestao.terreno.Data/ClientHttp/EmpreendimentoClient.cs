using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.terreno.Data.ClientHttp.Core;
using web.api.demarcacao.gestao.terreno.Data.ClientHttp.Response;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.ClientHttp;

namespace web.api.demarcacao.gestao.terreno.Data.ClientHttp
{
    public class EmpreendimentoClient : BaseClient, IEmpreendimentoClient
    {
        public EmpreendimentoClient(HttpClient httpClient, IUsuarioClient usuarioClient) : base(httpClient)
        {
        }
        public async Task<IEmpreendimentoResponse> GetEmpreendimentoAsync(long id, CancellationToken cancellationToken = default)
        {
            return await GetAsync<EmpreendimentoResponse>($"/api/v1/empreendimento/{id}");
        }
    }
}
