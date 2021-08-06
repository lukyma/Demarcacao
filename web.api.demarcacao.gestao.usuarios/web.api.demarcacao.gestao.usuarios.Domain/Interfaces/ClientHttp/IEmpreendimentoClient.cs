using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace web.api.demarcacao.gestao.usuarios.Domain.Interfaces.ClientHttp
{
    public interface IEmpreendimentoClient
    {
        Task<IEmpreendimentoResponse> GetEmpreendimentoAsync(long id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
