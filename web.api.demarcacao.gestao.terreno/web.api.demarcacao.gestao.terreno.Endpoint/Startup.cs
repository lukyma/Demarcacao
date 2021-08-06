using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Prometheus;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using web.api.demarcacao.gestao.terreno.Data.Context;
using web.api.demarcacao.gestao.terreno.Endpoint.Config.Swagger.Security;
using web.api.demarcacao.gestao.terreno.Endpoint.Helpers.AuthHandler;
using web.api.demarcacao.gestao.terreno.Endpoint.Helpers.AuthHandler.Requirement;
using web.api.demarcacao.gestao.terreno.Endpoint.Helpers.Middleware;
using web.api.demarcacao.gestao.terreno.Endpoint.Models.Automapper;
using web.api.demarcacao.gestao.terreno.Endpoint.Models.HandleValidaiton;
using web.api.demarcacao.gestao.terreno.IoC;
using web.api.demarcacao.gestao.terreno.Service.Automapper;

namespace web.api.demarcacao.gestao.terreno.Endpoint
{
    /// <summary>
    /// Classe startup
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">ServiceCollection for register dependency injection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
            .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.Formatting = Formatting.Indented;
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressMapClientErrors = true;
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(
            c =>
            {
                c.ExampleFilters();
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Demarcacao Terreno API",
                        Version = "v1",
                        Description = @"API que sera responsavel por fazer a gestao de terrenos"
                    });

                c.AddSecurityDefinition("Bearer", new BearerJwtSecuritySchema());
                c.AddSecurityRequirement(new BearerJwtSecurityRequirement());

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddHealthChecks()
                .AddNpgSql(Environment.GetEnvironmentVariable("ConnectionString"),
                           failureStatus: HealthStatus.Degraded,
                           timeout: TimeSpan.FromSeconds(5),
                           tags: new string[] { "db", "sql", "npgsql" })
                .AddRedis(Environment.GetEnvironmentVariable("RedisConnection"),
                           failureStatus: HealthStatus.Degraded,
                           timeout: TimeSpan.FromSeconds(5),
                           tags: new string[] { "db", "nosql", "redis" });

            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = "GestaTerreno-";
                options.Configuration = Environment.GetEnvironmentVariable("RedisConnection");
            });


            services.RegisterDependencies<IDemarcacaoGestaoTerrenoPostgressContext, DemarcacaoGestaoTerrenoPostgressContext>(Configuration);

            services.AddAutoMapper(typeof(RequestToEntityProfile),
                                   typeof(ModelToRequestProfile));

            services.AddScoped<IHandleValidation, HandleValidation>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CadTerr", policy => policy.Requirements.Add(new InterfaceRequirement("CAD_TERR")));
                options.AddPolicy("AtualTerr", policy => policy.Requirements.Add(new InterfaceRequirement("ATUAL_TERR")));
                options.AddPolicy("ExcTerr", policy => policy.Requirements.Add(new InterfaceRequirement("EXC_TERR")));
                options.AddPolicy("ListTerr", policy => policy.Requirements.Add(new InterfaceRequirement("LIST_TERR")));
            });

            services.AddScoped<IAuthorizationHandler, AcaoPermissaoHandler>();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("secretJwt"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSingleton<IList<IpRule>, List<IpRule>>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                                            $"Demarcacao Terreno API {description.GroupName.ToUpperInvariant()}");
                }
            });

            app.UseRouting();

            app.UseHttpMetrics();

            //app.UseMiddleware<MaxRequestPerMinutesMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });

            app.UseHealthChecks("/health/ready",
               new HealthCheckOptions()
               {
                   ResponseWriter = async (context, report) =>
                   {
                       var result = JsonConvert.SerializeObject(
                       new
                       {
                           statusApplication = report.Status.ToString(),
                           healthChecks = report.Entries.Select(e => new
                           {
                               checkName = e.Key,
                               errorMessage = e.Value.Exception?.Message,
                               status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                               exception = e.Value.Exception,
                               tags = e.Value.Tags
                           })
                       }, new JsonSerializerSettings()
                       {
                           ContractResolver = new CamelCasePropertyNamesContractResolver(),
                           NullValueHandling = NullValueHandling.Ignore,
                           Formatting = Formatting.Indented
                       });
                       context.Response.ContentType = MediaTypeNames.Application.Json;
                       await context.Response.WriteAsync(result);
                   }
               });

            app.UseHealthChecks("/health/live");
            app.UseHealthChecks("/health");
        }
    }
}
