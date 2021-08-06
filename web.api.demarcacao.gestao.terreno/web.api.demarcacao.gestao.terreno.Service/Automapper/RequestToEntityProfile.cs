using AutoMapper;
using web.api.demarcacao.gestao.terreno.Domain.Entities;
using web.api.demarcacao.gestao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.terreno.Service.Automapper
{
    public class RequestToEntityProfile : Profile
    {
        public RequestToEntityProfile()
        {
            CreateMap<CadastraTerrenoRequest, Terreno>();
            CreateMap<CoordenadaRequest, Coordenada>();
            CreateMap<Terreno, RetornaTerrenoQueryResponse>();
            CreateMap<Coordenada, CoordenadaRequest>();
            CreateMap<AtualizaTerrenoRequest, Terreno>();
            CreateMap<Terreno, TerrenoRequest>();
            CreateMap<Terreno, TerrenoResponse>();
        }
    }
}
