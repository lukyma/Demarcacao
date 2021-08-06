using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pattern.strategy;
using patterns.strategy;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using web.api.demarcacao.gestao.terreno.CrossCutting.Core;
using web.api.demarcacao.gestao.terreno.Data;
using web.api.demarcacao.gestao.terreno.Data.ClientHttp;
using web.api.demarcacao.gestao.terreno.Data.ClientHttp.Core;
using web.api.demarcacao.gestao.terreno.Data.Context;
using web.api.demarcacao.gestao.terreno.Data.Repository;
using web.api.demarcacao.gestao.terreno.Data.Repository.Common;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.ClientHttp;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.Repository;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.Repository.Common;
using web.api.demarcacao.gestao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.terreno.IoC
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
            services.RegisterClientHttp();
            return services;
        }

        private static void RegisterItensSetup(this IServiceCollection services)
        {
            services.AddSingleton<IProxyGenerator, ProxyGenerator>();
            services.AddScoped<IAsyncValidatorInterceptor, ValidatorInterceptor>();
            services.AddScoped<IValidationErrors, ValidationErrors>();
        }

        private static void RegisterStrategies(this IServiceCollection services)
        {
            services.AddScoppedStrategy<IStrategy<CadastraTerrenoRequest, CadastraTerrenoResponse>, CadastraTerrenoStrategy>();
            services.AddScoppedStrategy<IStrategy<RetornaTerrenoQuery, RetornaTerrenoQueryResponse>, RetornaTerrenoStrategy>();
            services.AddScoppedStrategy<IStrategy<AtualizaTerrenoRequest, DefaultResponse>, AtualizaTerrenoStrategy>();
            services.AddScoppedStrategy<IStrategy<ExcluiTerrenoRequest, DefaultResponse>, ExcluiTerrenoStrategy>();
            services.AddScoppedStrategy<IStrategy<ListaTerrenoQuery, ListaTerrenoQueryResponse>, ListaTerrenoStrategy>();
        }

        private static void RegisterItensDataBase<TContextService, TContextImplementation>(this IServiceCollection services)
            where TContextImplementation : DbContext, TContextService
            where TContextService : IDemarcacaoPostgressContext
        {
            services.AddDbContextPool<TContextImplementation>(options =>
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
            services.AddScoped<IDemarcacaoUnitOfWork, DemarcacaoUnitOfWork<TContextImplementation>>();
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,,>));
            services.AddScoped<ITerrenoRepository, TerrenoRepository<TContextImplementation>>();
            services.AddScoped<ICoordenadaRepository, CoordenadaRepository<TContextImplementation>>();
        }

        private static void RegisterClientHttp(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITokenGestaoTerreno, TokenGestaoTerreno>();

            ServicePointManager.ServerCertificateValidationCallback =
             delegate (
                 object s,
                 X509Certificate certificate,
                 X509Chain chain,
                 SslPolicyErrors sslPolicyErrors
             )
             {
                 return true;
             };

            services.AddHttpClient<IUsuarioClient, UsuarioClient>((sp, httpClient) =>
                    {
                        httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("HttpClientUsuario"));
                    })
                .ConfigurePrimaryHttpMessageHandler((sp) => new HttpClientAccessTokenHandler(sp.GetRequiredService<ITokenGestaoTerreno>()));

            services.AddHttpClient<IEmpreendimentoClient, EmpreendimentoClient>((httpClient) =>
                    {
                        httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("HttpClientEmpreendimento"));
                    }).ConfigurePrimaryHttpMessageHandler((sp) => new HttpClientAccessTokenHandler(sp.GetRequiredService<ITokenGestaoTerreno>()))
                    .AddPolicyHandler((sp, requestMessage) =>
                    {
                        return PollyExtensions.GetRetryAuthPolicyAsync((user, pass) =>
                        {
                            return sp.GetRequiredService<IUsuarioClient>().SetAccessTokenAsync(user, pass);
                        });
                    });

        }
    }
}
