using web.api.demarcacao.gestao.usuarios.Domain.Entities;
using web.api.demarcacao.gestao.usuarios.Domain.Interfaces.Repository.Common;

namespace web.api.demarcacao.gestao.usuarios.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario, long>
    {
    }
}
