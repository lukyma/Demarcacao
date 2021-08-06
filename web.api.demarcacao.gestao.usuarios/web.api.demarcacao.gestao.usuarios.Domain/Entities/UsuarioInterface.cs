namespace web.api.demarcacao.gestao.usuarios.Domain.Entities
{
    public class UsuarioInterface
    {
        public UsuarioInterface(long idUsuario, long idInterface)
        {
            IdUsuario = idUsuario;
            IdInterface = idInterface;
        }
        public long IdUsuario { get; set; }
        public long IdInterface { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Interface Interface { get; set; }
    }
}
