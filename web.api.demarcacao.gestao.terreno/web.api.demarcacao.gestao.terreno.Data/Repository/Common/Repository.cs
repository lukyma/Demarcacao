using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.terreno.Data.Context;
using web.api.demarcacao.gestao.terreno.Domain.Entities.Core;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.Repository.Common;

namespace web.api.demarcacao.gestao.terreno.Data.Repository.Common
{
    public class Repository<TEntity, TKey, TContext> : IRepository<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>
        where TContext : IDemarcacaoPostgressContext
    {
        private TContext _dbContext { get; }
        private DbSet<TEntity> _dbSet { get; }

        public Repository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        protected TContext Context
        {
            get { return _dbContext; }
        }

        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }
        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return DbSet.AddAsync(entity, cancellationToken).AsTask();
        }

        public virtual void Delete(TKey key)
        {
            DbSet.Remove(DbSet.Find(key));
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public TEntity Get(TKey id)
        {
            return DbSet.Find(id);
        }

        public Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken)
        {
            return DbSet.FindAsync(new object[] { id }, cancellationToken).AsTask();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Update(entity);
        }

        public virtual IQueryable<TEntity> All(int page, int take, bool @readonly = false)
        {
            take = take == 0 ? 10 : take;
            page = (page - 1) < 0 ? 1 : page;
            return @readonly
                ? DbSet.Skip((page - 1) * take).Take(take).AsNoTracking()
                : DbSet.Skip((page - 1) * take).Take(take);
        }
        public virtual async Task<IEnumerable<TEntity>> AllAsync(int page, int take, CancellationToken cancellationToken, bool @readonly = false)
        {
            take = take <= 0 ? 10 : take;
            page = (page - 1) < 0 ? 1 : page;
            return @readonly
                ? await DbSet.Skip((page - 1) * take).Take(take).AsNoTracking().ToListAsync(cancellationToken)
                : await DbSet.Skip((page - 1) * take).Take(take).ToListAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false)
        {
            return @readonly
                ? DbSet.Where(predicate).AsNoTracking()
                : DbSet.Where(predicate);
        }

        public bool TryGet(TKey id, out TEntity entity)
        {
            entity = DbSet.Find(id);
            if (entity != null)
            {
                return true;
            }
            return false;
        }
    }
}
