using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.empreendimento.Service.Application.Validators
{
    public class EmpreendimentoRequestValidator<TRequest> : AbstractValidator<TRequest> where TRequest : EmpreendimentoRequest
    {
        public List<string> CamposObrigatorios { get; }
        public EmpreendimentoRequestValidator()
        {
            CamposObrigatorios = new List<string>();
            CascadeMode = CascadeMode.Stop;
            RuleFor(o => o)
                .Must(ValidarCampoObrigatorios)
                .WithMessage((request) => $"Os seguintes campos obrigatórios não foram informados: {string.Join(',', CamposObrigatorios)}")
                .WithErrorCode("001");
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
                {nameof(request.GrupoEmpresa), request.GrupoEmpresa},
                {$"{nameof(request.Endereco)}.{ nameof(request.Endereco.Logradouro)}", request.Endereco.Logradouro},
                {$"{nameof(request.Endereco)}.{nameof(request.Endereco.Numero)}", request.Endereco.Numero},
                {$"{nameof(request.Endereco)}.{nameof(request.Endereco.Bairro)}", request.Endereco.Bairro},
                {$"{nameof(request.Endereco)}.{nameof(request.Endereco.Cidade)}", request.Endereco.Cidade},
                {$"{nameof(request.Endereco)}.{nameof(request.Endereco.Estado)}", request.Endereco.Estado},
                {$"{nameof(request.Endereco)}.{nameof(request.Endereco.Cep)}", request.Endereco.Cep}
            };
        }
    }
}
