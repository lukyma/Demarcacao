using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using web.api.demarcacao.gestao.usuarios.Endpoint.Models;

namespace web.api.demarcacao.gestao.usuarios.Endpoint.Config.Swagger
{
    [ExcludeFromCodeCoverage]
    public class SwaggerErrorMessages
    {
        public class DefaultMessage
        {
            protected IEnumerable<Error> Erros { get; }
            public DefaultMessage()
            {
                Erros = new List<Error>()
                {
                    new Error("999", "Ocorreu uma falha ao processar a solicitação")
                };
            }
        }
    }
}
