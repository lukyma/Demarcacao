using System.Collections.Generic;
using web.api.demarcacao.gestao.usuarios.Domain.Entities.Core;

namespace web.api.demarcacao.gestao.usuarios.Domain.Entities
{
    public class Usuario : AggregateRoot<long>
    {
        public Usuario(string nome, string login, string password)
        {
            Nome = nome;
            Login = login;
            Password = password;
            UsuarioInterfaces = new HashSet<UsuarioInterface>();
        }
        public Usuario(long id, string nome, string login, string password)
        {
            Id = id;
            Nome = nome;
            Login = login;
            Password = password;
            UsuarioInterfaces = new HashSet<UsuarioInterface>();
        }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual ICollection<UsuarioInterface> UsuarioInterfaces { get; set; }
    }
}
