using Swashbuckle.AspNetCore.Filters;
using System.Linq;
using web.api.demarcacao.gestao.empreendimento.Endpoint.Models;
using static web.api.demarcacao.gestao.empreendimento.Endpoint.Config.Swagger.SwaggerErrorMessages;

namespace web.api.demarcacao.gestao.empreendimento.Endpoint.Config.Swagger
{
    public class EmpreendimentoExampleMessage
    {
        public class EmpreendimentoRequestMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                var listErros = Erros.ToList();
                listErros.Add(new Error("001", "Os seguintes campos obrigatórios não foram informados: <campos separados por vírgula>"));
                return new ErrorMessage(listErros);
            }
        }

        public class EmpreendimentoDeleteMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                return new ErrorMessage(Erros);
            }
        }

        public class EmpreendimentoGetIdMessage : DefaultMessage, IExamplesProvider<ErrorMessage>
        {
            public ErrorMessage GetExamples()
            {
                return new ErrorMessage(Erros);
            }
        }
    }
}
