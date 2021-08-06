using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace web.api.demarcacao.gestao.empreendimento.Endpoint.Config.Swagger.Security
{
    /// <summary>
    /// JWT Bearer token requirement swagger.
    /// </summary>
    [Serializable]
    public class BearerJwtSecurityRequirement : OpenApiSecurityRequirement
    {
        public BearerJwtSecurityRequirement()
        {
            Add(new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            }, new List<string>());
        }
    }
}
