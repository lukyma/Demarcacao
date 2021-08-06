using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace web.api.demarcacao.gestao.terreno.Data.Context.Factory
{
    public class DemarcacaoGestaoTerrenoPostgressContextFactory : IDesignTimeDbContextFactory<DemarcacaoGestaoTerrenoPostgressContext>
    {
        public DemarcacaoGestaoTerrenoPostgressContext CreateDbContext(string[] args)
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("ConnectionString"));
            var optionsBuilder = new DbContextOptionsBuilder<DemarcacaoGestaoTerrenoPostgressContext>();
            var connectionString = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ConnectionString")) ?
                                       $"Server={Environment.GetEnvironmentVariable("hostDd")};" +
                                       $"Port={Environment.GetEnvironmentVariable("portDb")};" +
                                       $"User Id={Environment.GetEnvironmentVariable("userNameDb")};" +
                                       $"Password={Environment.GetEnvironmentVariable("passwordDb")};" +
                                       $"Database={Environment.GetEnvironmentVariable("databaseNameDb")};" +
                                       $"SSL Mode=Prefer;Trust Server Certificate=true" :
                                       Environment.GetEnvironmentVariable("ConnectionString");
            optionsBuilder.UseNpgsql(connectionString);
            return new DemarcacaoGestaoTerrenoPostgressContext(optionsBuilder.Options);
        }
    }

}
