using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using web.api.demarcacao.gestao.usuarios.CrossCutting;
using web.api.demarcacao.gestao.usuarios.Service.Application.Strategy;

namespace web.api.demarcacao.gestao.usuarios.Endpoint.Helpers.AuthHandler
{
    public static class GegerateToken
    {
        public static string Generate(AuthUserQueryResponse user)
        {
            var clains = new Dictionary<string, string>();
            user.Interfaces.ForEach(o =>
            {
                var interfaceSplit = o.Split(";");
                clains.Add(interfaceSplit[0], interfaceSplit[1]);
            });
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("secretJwt"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", user.IdUsuario.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(3),
                Claims = new Dictionary<string, object>()
                {
                    { "interfaces", clains }
                },
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
