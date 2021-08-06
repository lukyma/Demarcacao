using web.api.demarcacao.gestao.terreno.Domain.Interfaces.ClientHttp;

namespace web.api.demarcacao.gestao.terreno.Data.ClientHttp
{
    public class TokenGestaoTerreno : ITokenGestaoTerreno
    {
        private readonly object balanceLock = new object();
        private string _AccessToken { get; set; }
        public string AccessToken
        {
            get
            {
                return _AccessToken;
            }
            set
            {
                lock (balanceLock)
                {
                    _AccessToken = value;
                }
            }
        }
    }
}
