using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.ClientHttp;

namespace web.api.demarcacao.gestao.terreno.Data.ClientHttp.Core
{
    public class HttpClientAccessTokenHandler : HttpClientHandler
    {
        private ITokenGestaoTerreno TokenGestaoTerreno { get; }
        public HttpClientAccessTokenHandler(ITokenGestaoTerreno tokenGestaoTerreno)
        {
            ClientCertificateOptions = ClientCertificateOption.Manual;
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
            TokenGestaoTerreno = tokenGestaoTerreno;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", TokenGestaoTerreno.AccessToken);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
