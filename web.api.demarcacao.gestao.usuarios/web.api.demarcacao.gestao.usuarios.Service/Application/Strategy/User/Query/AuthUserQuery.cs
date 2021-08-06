namespace web.api.demarcacao.gestao.usuarios.Service.Application.Strategy
{
    public struct AuthUserQuery
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public struct AuthUserQueryResponse
    {
        public AuthUserQueryResponse(long idUsuario, string[] interfaces, bool autenticado = true)
        {
            IdUsuario = idUsuario;
            Interfaces = interfaces;
            Autenticado = autenticado;
        }
        public long IdUsuario { get; set; }
        public string[] Interfaces { get; set; }
        public bool Autenticado { get; set; }
    }
}
