using Microsoft.AspNetCore.Authorization;

namespace web.api.demarcacao.gestao.empreendimento.Endpoint.Helpers.AuthHandler.Requirement
{
    public struct InterfaceRequirement : IAuthorizationRequirement
    {
        public string Tag { get; }
        public InterfaceRequirement(string tag)
        {
            Tag = tag;
        }
    }
}
