using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.terreno.Domain.Entities.Core;

namespace web.api.demarcacao.gestao.terreno.Domain.Interfaces.Repository.Common
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>

    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity Get(TKey id);
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken);
        bool TryGet(TKey id, out TEntity entity);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> All(int page, int take, bool @readonly = false);
        Task<IEnumerable<TEntity>> AllAsync(int page, int take, CancellationToken cancellationToken, bool @readonly = false);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
    }
}
