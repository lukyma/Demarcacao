using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.api.demarcacao.gestao.empreendimento.Endpoint.Helpers.Middleware
{
    public class MaxRequestPerMinutesMiddleware
    {
        private readonly RequestDelegate _next;

        public MaxRequestPerMinutesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IList<IpRule> ipRules, IHttpContextAccessor httpContextAccessor)
        {
            var id = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
            var ipRule = ipRules.FirstOrDefault(o => o.Id == id);
            if (ipRule == null)
            {
                ipRules.Add(new IpRule() { Id = id, Expiration = DateTime.Now.AddMinutes(1), Requests = 1 });
                await _next(context);
            }
            else
            {
                if (ipRule.ValidarIpAcessoPorMinuto())
                {
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync($"Só é permitido fazer 10 chamadas no intervalo de um minuto. {ipRule.Id} \n" +
                                                      $"Você poderá fazer novas chamadas em {ipRule.Expiration.Subtract(DateTime.Now).Seconds} segundos");
                }
            }
        }
    }

    public class IpRule
    {
        public const short AcessosPermitidosPorMiniuto = 10;
        public string Id { get; set; }
        public int Requests { get; set; }
        public DateTime Expiration { get; set; }

        public bool ValidarIpAcessoPorMinuto()
        {
            if (Requests < AcessosPermitidosPorMiniuto)
            {
                Requests += 1;
                return true;
            }
            else if (Expiration > DateTime.Now && Requests == AcessosPermitidosPorMiniuto)
            {
                return false;
            }
            else if (Expiration <= DateTime.Now)
            {
                Requests = 1;
                Expiration = DateTime.Now.AddMinutes(1);
                return true;
            }

            return false;
        }
    }
}
