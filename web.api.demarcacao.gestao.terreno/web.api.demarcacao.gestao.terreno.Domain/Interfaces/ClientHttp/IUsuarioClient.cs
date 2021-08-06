using System.Threading;
using System.Threading.Tasks;

namespace web.api.demarcacao.gestao.terreno.Domain.Interfaces.ClientHttp
{
    public interface IUsuarioClient
    {
        Task SetAccessTokenAsync(string user, string pass, CancellationToken cancellationToken = default(CancellationToken));
    }
}
