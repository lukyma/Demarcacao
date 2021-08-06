using AutoMapper;
using web.api.demarcacao.gestao.empreendimento.Domain.Entities;
using web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.empreendimento.Service.Automapper
{
    public class RequestToEntityProfile : Profile
    {
        public RequestToEntityProfile()
        {
            CreateMap<CadastraEmpreendimentoRequest, Empreendimento>();
            CreateMap<AtualizaEmpreendimentoRequest, Empreendimento>()
                .ForMember(o => o.Id, o => o.MapFrom(o => o.IdEmpreendimento));
            CreateMap<Empreendimento, RetornarEmpreendimentoQueryResponse>()
                .ForMember(o => o.Id, o => o.MapFrom(p => p.Id));
            CreateMap<Empreendimento, EmpreendimentoRequest>();
            CreateMap<Empreendimento, EmpreendimentoResponse>()
                .ForMember(o => o.Id, o => o.MapFrom(p => p.Id));
        }
    }
}
