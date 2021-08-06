using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.terreno.Data.ClientHttp.Core;
using web.api.demarcacao.gestao.terreno.Data.ClientHttp.Response;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.ClientHttp;

namespace web.api.demarcacao.gestao.terreno.Data.ClientHttp
{
    public class UsuarioClient : BaseClient, IUsuarioClient
    {
        private ITokenGestaoTerreno TokenGestaoTerreno { get; }
        public UsuarioClient(HttpClient httpClient, ITokenGestaoTerreno tokenGestaoTerreno) : base(httpClient)
        {
            TokenGestaoTerreno = tokenGestaoTerreno;
        }

        public async Task SetAccessTokenAsync(string user, string pass, CancellationToken cancellationToken = default)
        {
            var response = await PostAsync<AutenticacaoResponse>(new { login = user, password = pass }, "/api/v1/auth");
            TokenGestaoTerreno.AccessToken = response.AccessToken;
        }
    }
}
