using Microsoft.OpenApi.Models;

namespace web.api.demarcacao.gestao.usuarios.Endpoint.Config.Swagger.Security
{
    public class BearerJwtSecuritySchema : OpenApiSecurityScheme
    {
        public BearerJwtSecuritySchema()
        {
            Description = @"Autorização JWT";
            Name = "Authorization";
            In = ParameterLocation.Header;
            Type = SecuritySchemeType.ApiKey;
            Scheme = "Bearer";
            BearerFormat = "JWT";
        }
    }
}
