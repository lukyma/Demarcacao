using System.Collections.Generic;
using web.api.demarcacao.gestao.usuarios.Domain.Entities.Core;

namespace web.api.demarcacao.gestao.usuarios.Domain.Entities
{
    public class Interface : AggregateRoot<long>
    {
        public Interface()
        {
            UsuarioInterfaces = new HashSet<UsuarioInterface>();
        }
        public Interface(long id, string descricao, string tag)
        {
            Id = id;
            Descricao = descricao;
            Tag = tag;
            UsuarioInterfaces = new HashSet<UsuarioInterface>();
        }
        public string Descricao { get; set; }
        public string Tag { get; set; }
        public virtual ICollection<UsuarioInterface> UsuarioInterfaces { get; set; }
    }
}
