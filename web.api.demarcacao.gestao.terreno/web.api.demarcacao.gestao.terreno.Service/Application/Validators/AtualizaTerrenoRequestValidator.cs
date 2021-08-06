using System.Collections.Generic;
using web.api.demarcacao.gestao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.terreno.Service.Application.Validators
{
    public class AtualizaTerrenoRequestValidator : TerrenoRequestValidator<AtualizaTerrenoRequest>
    {
        public AtualizaTerrenoRequestValidator()
        {
        }

        protected override IDictionary<string, string> RetornarStringsObrigatorias(AtualizaTerrenoRequest request)
        {
            var response = base.RetornarStringsObrigatorias(request);
            response.Add(nameof(request.Id), request.Id == 0 ? "" : request.Id.ToString());
            return response;
        }
    }
}
