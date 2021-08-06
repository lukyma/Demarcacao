using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using web.api.demarcacao.gestao.terreno.CrossCutting;
using web.api.demarcacao.gestao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.terreno.Service.Application.Validators
{
    public class TerrenoRequestValidator<TRequest> : AbstractValidator<TRequest> where TRequest : TerrenoRequest
    {
        public List<string> CamposObrigatorios { get; }
        public TerrenoRequestValidator()
        {
            CamposObrigatorios = new List<string>();
            CascadeMode = CascadeMode.Stop;
            RuleFor(o => o)
                .Must(ValidarCampoObrigatorios)
                .WithMessage((request) => $"Os seguintes campos obrigatórios não foram informados: {string.Join(',', CamposObrigatorios)}")
                .WithErrorCode("001")
                .Must(o => o.Coordenadas != null && o.Coordenadas.Count() >= 2)
                .WithMessage("Ao menos duas coordenadas devem ser informadas")
                .WithErrorCode("002");
        }

        private bool ValidarCampoObrigatorios(TRequest request)
        {
            var validacaoCampos = RetornarStringsObrigatorias(request).Select(ValidarCampos).ToList();
            if (request == null || validacaoCampos.Any(o => !o))
            {
                return false;
            }
            return true;
        }

        private bool ValidarCampos(KeyValuePair<string, string> keyValue)
        {
            if (string.IsNullOrEmpty(keyValue.Value))
            {
                CamposObrigatorios.Add(keyValue.Key);
                return false;
            }

            return true;
        }

        protected virtual IDictionary<string, string> RetornarStringsObrigatorias(TRequest request)
        {
            return new Dictionary<string, string>()
            {
                {nameof(request.Descricao), request.Descricao},
                {nameof(request.IdEmpreendimento), request.IdEmpreendimento.ToString().GetEmptyIfNumber0()}
            };
        }
    }
}
