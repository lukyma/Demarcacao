using Swashbuckle.AspNetCore.Filters;
using System.Linq;
using web.api.demarcacao.gestao.usuarios.Endpoint.Models;
using static web.api.demarcacao.gestao.usuarios.Endpoint.Config.Swagger.SwaggerErrorMessages;

namespace web.api.demarcacao.gestao.usuarios.Endpoint.Config.Swagger
{
    public class TerrenoExampleMessage
    {
        public class TerrenoRequestMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                var listErros = Erros.ToList();
                listErros.Add(new Error("001", "Os seguintes campos obrigatórios não foram informados: <campos separados por vírgula>"));
                listErros.Add(new Error("002", "Ao menos duas coordenadas devem ser informadas"));
                return new ErrorMessage(listErros);
            }
        }

        public class TerrenoDeleteMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                return new ErrorMessage(Erros);
            }
        }

        public class TerrenoGetIdMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                return new ErrorMessage(Erros);
            }
        }
    }
}
