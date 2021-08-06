using AutoMapper;
using web.api.demarcacao.gestao.terreno.Endpoint.Models.Reseponse;
using web.api.demarcacao.gestao.terreno.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.terreno.Endpoint.Models.Automapper
{
    public class ModelToRequestProfile : Profile
    {
        public ModelToRequestProfile()
        {
            CreateMap<TerrenoRequest, TerrenoVM>();
            CreateMap<TerrenoResponse, TerrenoResponseVM>();
            CreateMap<TerrenoVM, CadastraTerrenoRequest>();
            CreateMap<CoordenadasVM, CoordenadaRequest>();
            CreateMap<CoordenadaRequest, CoordenadasVM>();
            CreateMap<TerrenoVM, AtualizaTerrenoRequest>();
            CreateMap<ListaTerrenoQueryResponse, ListaTerrenoResponseVM>();
        }
    }
}
