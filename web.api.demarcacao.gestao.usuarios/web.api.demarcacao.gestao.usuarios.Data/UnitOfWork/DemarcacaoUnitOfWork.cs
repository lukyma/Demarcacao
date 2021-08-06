﻿using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.usuarios.CrossCutting.Core;
using web.api.demarcacao.gestao.usuarios.Data.Context;

namespace web.api.demarcacao.gestao.usuarios.Data
{
    public class DemarcacaoUnitOfWork<TContext> : IDemarcacaoUnitOfWork
        where TContext : IDemarcacaoPostgressContext
    {
        private TContext DbContext { get; }

        public DemarcacaoUnitOfWork(TContext context)
        {
            DbContext = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return DbContext.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
