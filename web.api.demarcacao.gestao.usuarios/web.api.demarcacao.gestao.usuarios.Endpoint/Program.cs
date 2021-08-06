using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using web.api.demarcacao.gestao.usuarios.Data.Context;

namespace web.api.demarcacao.gestao.usuarios.Endpoint
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().MigrateDatabase<IDemarcacaoPostgressContext>().Run();
        }

        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<TContext>() as DbContext;
                    db.Database.ExecuteSqlRaw("SELECT * from version()");
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return host;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
