using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace web.api.demarcacao.gestao.terreno.Data.ClientHttp.Core
{
    public static class PollyExtensions
    {
        public static volatile string AccessToken;
        public static IAsyncPolicy<HttpResponseMessage> GetRetryAuthPolicyAsync(Func<string, string, Task> func)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                .RetryAsync(1, async (message, retryCount, context) =>
                {
                    await func(Environment.GetEnvironmentVariable("User"), Environment.GetEnvironmentVariable("Pass"));
                });
        }
    }
}
