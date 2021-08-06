using AutoMapper;
using web.api.demarcacao.gestao.empreendimento.Domain.Entities;
using web.api.demarcacao.gestao.empreendimento.Endpoint.Models.Reseponse;
using web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.empreendimento.Endpoint.Models.Automapper
{
    public class ModelToRequestProfile : Profile
    {
        public ModelToRequestProfile()
        {
            CreateMap<EnderecoVM, Endereco>();
            CreateMap<Endereco, EnderecoVM>();
            CreateMap<EmpreendimentoVM, CadastraEmpreendimentoRequest>();
            CreateMap<EmpreendimentoVM, AtualizaEmpreendimentoRequest>();
            CreateMap<RetornarEmpreendimentoQueryResponse, EmpreendimentoResponseVM>();
            CreateMap<RetornarEmpreendimentoQueryResponse, EmpreendimentoVM>();
            CreateMap<ListaEmpreendimentoQueryResponse, ListaEmpreendimentoResponseVM>();
            CreateMap<EmpreendimentoRequest, EmpreendimentoVM>();
            CreateMap<EmpreendimentoResponse, EmpreendimentoResponseVM>();
        }
    }
}
