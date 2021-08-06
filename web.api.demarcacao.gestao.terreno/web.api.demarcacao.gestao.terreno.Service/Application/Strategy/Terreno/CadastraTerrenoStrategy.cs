using AutoMapper;
using FluentValidation.Results;
using pattern.strategy;
using patterns.strategy;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.terreno.CrossCutting.Core;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.ClientHttp;
using web.api.demarcacao.gestao.terreno.Domain.Interfaces.Repository;
using web.api.demarcacao.gestao.terreno.Service.Application.Validators;

namespace web.api.demarcacao.gestao.terreno.Service.Application.Strategy
{
    public class CadastraTerrenoStrategy : IStrategy<CadastraTerrenoRequest, CadastraTerrenoResponse>
    {
        private IMapper Mapper { get; }
        private ITerrenoRepository TerrenoRepository { get; }
        private IDemarcacaoUnitOfWork UnitOfWork { get; }
        private IValidationErrors ValidationFailures { get; }
        private IEmpreendimentoClient EmpreendimentoClient { get; }
        public CadastraTerrenoStrategy(IMapper mapper,
                                       ITerrenoRepository terrenoRepository,
                                       IEmpreendimentoClient empreendimentoClient,
                                       IValidationErrors validationErrors,
                                       IDemarcacaoUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            TerrenoRepository = terrenoRepository;
            EmpreendimentoClient = empreendimentoClient;
            ValidationFailures = validationErrors;
            UnitOfWork = unitOfWork;
        }

        [Validator(typeof(TerrenoRequestValidator<CadastraTerrenoRequest>))]
        public async Task<CadastraTerrenoResponse> HandleAsync(CadastraTerrenoRequest request, CancellationToken cancellationToken)
        {
            var retorno = await EmpreendimentoClient.GetEmpreendimentoAsync(request.IdEmpreendimento);

            if (retorno == default || retorno?.Id == 0)
            {
                ValidationFailures.Add(new ValidationFailure("", "Não existe empreendimento conforme conforme id informado") { ErrorCode = "004" });
            }
            var terrenoEntity = Mapper.Map<Domain.Entities.Terreno>(request);
            await TerrenoRepository.AddAsync(terrenoEntity, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            return new CadastraTerrenoResponse(terrenoEntity.Id);
        }
    }
}
