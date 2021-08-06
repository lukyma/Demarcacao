using FluentValidation;
using web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.empreendimento.Service.Application.Validators
{
    public class RetornaEmpreendimentoQueryValidator : AbstractValidator<RetornaEmpreendimentoQuery>
    {
        public RetornaEmpreendimentoQueryValidator()
        {
            RuleFor(o => o.IdEmpreendimento)
                .NotEmpty()
                .WithMessage("O campo \"idEmpreendimento\" não foi informado.")
                .WithErrorCode("001");
        }
    }
}
