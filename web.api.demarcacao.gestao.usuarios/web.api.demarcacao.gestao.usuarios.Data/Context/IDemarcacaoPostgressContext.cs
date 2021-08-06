using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace web.api.demarcacao.gestao.usuarios.Data.Context
{
    public interface IDemarcacaoPostgressContext
    {
        DatabaseFacade Database { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry Entry(object entity);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry Update(object entity);
    }

    public interface IDemarcacaoGestaoUsuarioPostgressContext : IDemarcacaoPostgressContext
    {
    }
}
