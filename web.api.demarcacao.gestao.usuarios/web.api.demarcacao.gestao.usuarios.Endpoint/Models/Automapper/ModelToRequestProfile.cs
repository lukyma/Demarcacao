using AutoMapper;
using web.api.demarcacao.gestao.usuarios.Endpoint.Helpers.AuthHandler;
using web.api.demarcacao.gestao.usuarios.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.usuarios.Endpoint.Models.Automapper
{
    public class ModelToRequestProfile : Profile
    {
        public ModelToRequestProfile()
        {
            CreateMap<AuthTokenVM, AuthUserQuery>();
            CreateMap<AuthUserQueryResponse, AuthTokenResponseVM>()
                .ForMember(o => o.AccessToken, o => o.MapFrom(p => GegerateToken.Generate(p)));
        }
    }
}
