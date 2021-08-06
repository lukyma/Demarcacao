using System.Threading;
using System.Threading.Tasks;

namespace web.api.demarcacao.gestao.terreno.Domain.Interfaces.ClientHttp
{
    public interface IEmpreendimentoClient
    {
        Task<IEmpreendimentoResponse> GetEmpreendimentoAsync(long id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
