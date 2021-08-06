using System.Collections.Generic;
using web.api.demarcacao.gestao.Cross;
using web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.empreendimento.Service.Application.Validators
{
    public class AtualizaEmpreendimentoRequestValidator : EmpreendimentoRequestValidator<AtualizaEmpreendimentoRequest>
    {
        public AtualizaEmpreendimentoRequestValidator()
        {
        }

        protected override IDictionary<string, string> RetornarStringsObrigatorias(AtualizaEmpreendimentoRequest request)
        {
            var response = base.RetornarStringsObrigatorias(request);
            response.Add(nameof(request.IdEmpreendimento), request.IdEmpreendimento.ToString().GetEmptyIfNumber0());
            return response;
        }
    }
}
