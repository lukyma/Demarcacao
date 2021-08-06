using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace web.api.demarcacao.gestao.empreendimento.Data.Context.Factory
{
    public class DemarcacaoGestaoEmpreendimentoPostgressContextFactory : IDesignTimeDbContextFactory<DemarcacaoGestaoEmpreendimentoPostgressContext>
    {
        public DemarcacaoGestaoEmpreendimentoPostgressContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DemarcacaoGestaoEmpreendimentoPostgressContext>();
            var connectionString = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ConnectionString")) ?
                                       $"Server={Environment.GetEnvironmentVariable("hostDd")};" +
                                       $"Port={Environment.GetEnvironmentVariable("portDb")};" +
                                       $"User Id={Environment.GetEnvironmentVariable("userNameDb")};" +
                                       $"Password={Environment.GetEnvironmentVariable("passwordDb")};" +
                                       $"Database={Environment.GetEnvironmentVariable("databaseNameDb")};" +
                                       $"SSL Mode=Prefer;Trust Server Certificate=true" :
                                       Environment.GetEnvironmentVariable("ConnectionString");
            optionsBuilder.UseNpgsql(connectionString);
            return new DemarcacaoGestaoEmpreendimentoPostgressContext(optionsBuilder.Options);
        }
    }
}
