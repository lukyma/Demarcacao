using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using patterns.strategy;
using System;
using web.api.demarcacao.gestao.empreendimento.Cross.Core;
using web.api.demarcacao.gestao.empreendimento.Data;
using web.api.demarcacao.gestao.empreendimento.Data.Context;
using web.api.demarcacao.gestao.empreendimento.Data.Repository;
using web.api.demarcacao.gestao.empreendimento.Data.Repository.Common;
using web.api.demarcacao.gestao.empreendimento.Domain.Interfaces.Repository;
using web.api.demarcacao.gestao.empreendimento.Domain.Interfaces.Repository.Common;
using web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy;
using web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy.Request;

namespace web.api.demarcacao.gestao.empreendimento.IoC
{
    public static class DependecyIncectionRegistry
    {
        public static IServiceCollection RegisterDependencies<TContextService, TContextImplementation>(this IServiceCollection services, IConfiguration configuration)
            where TContextImplementation : DbContext, TContextService
            where TContextService : IDemarcacaoPostgressContext
        {
            services.RegisterItensSetup();
            services.RegisterItensDataBase<TContextService, TContextImplementation>();
            services.RegisterStrategies();
            return services;
        }

        private static void RegisterItensSetup(this IServiceCollection services)
        {
            services.AddSingleton<IProxyGenerator, ProxyGenerator>();
            services.AddScoped<IAsyncValidatorInterceptor, ValidatorInterceptor>();
        }

        private static void RegisterStrategies(this IServiceCollection services)
        {
            services.AddScoppedStrategy<IStrategy<CadastraEmpreendimentoRequest, DefaultResponse>, CadastraEmpreendimentoStrategy>();
            services.AddScoppedStrategy<IStrategy<AtualizaEmpreendimentoRequest, DefaultResponse>, AtualizaEmpreendimentoStrategy>();
            services.AddScoppedStrategy<IStrategy<RetornaEmpreendimentoQuery, RetornarEmpreendimentoQueryResponse>, RetornaEmpreendimentoStrategy>();
            services.AddScoppedStrategy<IStrategy<ListaEmpreendimentoQuery, ListaEmpreendimentoQueryResponse>, ListaEmpreendimentoStrategy>();
        }

        private static void RegisterItensDataBase<TContextService, TContextImplementation>(this IServiceCollection services)
            where TContextImplementation : DbContext, TContextService
            where TContextService : IDemarcacaoPostgressContext
        {
            services.AddDbContext<TContextService, TContextImplementation>(options =>
            {
                Console.WriteLine(Environment.GetEnvironmentVariable("ConnectionString"));
                var connectionString = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ConnectionString")) ?
                                       $"Server={Environment.GetEnvironmentVariable("hostDd")};" +
                                       $"Port={Environment.GetEnvironmentVariable("portDb")};" +
                                       $"User Id={Environment.GetEnvironmentVariable("userNameDb")};" +
                                       $"Password={Environment.GetEnvironmentVariable("passwordDb")};" +
                                       $"Database={Environment.GetEnvironmentVariable("databaseNameDb")};" +
                                       $"SSL Mode=Prefer;Trust Server Certificate=true" :
                                       Environment.GetEnvironmentVariable("ConnectionString");
                options.UseLazyLoadingProxies().UseNpgsql(connectionString);
            });
            services.AddScoped<IDemarcacaoUnitOfWork, DemarcacaoUnitOfWork<TContextService>>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,,>));
            services.AddScoped<IEmpreendimentoRepository, EmpreendimentoRepository<TContextService>>();
        }
    }
}
