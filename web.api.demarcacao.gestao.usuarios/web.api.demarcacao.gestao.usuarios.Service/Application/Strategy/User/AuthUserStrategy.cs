using patterns.strategy;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.usuarios.Domain.Interfaces.Repository;

namespace web.api.demarcacao.gestao.usuarios.Service.Application.Strategy
{
    public class AuthUserStrategy : IStrategy<AuthUserQuery, AuthUserQueryResponse>
    {
        private IUsuarioRepository UsuarioRepository { get; }
        public AuthUserStrategy(IUsuarioRepository usuarioRepository)
        {
            UsuarioRepository = usuarioRepository;
        }
        public async Task<AuthUserQueryResponse> HandleAsync(AuthUserQuery request, CancellationToken cancellationToken)
        {
            var usuarioFind = UsuarioRepository.Find(o => o.Login == request.Login && o.Password == request.Password);
            if (usuarioFind.Any())
            {
                var usuario = usuarioFind.FirstOrDefault();
                List<string> interfaces = new List<string>();
                foreach (var item in usuario.UsuarioInterfaces)
                {
                    interfaces.Add($"{item.Interface.Tag};{item.Interface.Descricao}");
                }
                return await Task.FromResult(new AuthUserQueryResponse(usuario.Id, interfaces.ToArray()));
            }
            else
            {
                return await Task.FromResult(new AuthUserQueryResponse(0, null, false));
            }
        }
    }
}
