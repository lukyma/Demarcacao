using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace web.api.demarcacao.gestao.terreno.CrossCutting.Core
{
    public interface IUnitOfWork
    {
        IDbContextTransaction BeginTransaction();
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
