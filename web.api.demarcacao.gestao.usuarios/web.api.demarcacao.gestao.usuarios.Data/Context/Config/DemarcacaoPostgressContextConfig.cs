using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;

namespace web.api.demarcacao.gestao.usuarios.Data.Context.Config
{
    class DemarcacaoPostgressContextConfig : DbConfiguration
    {
        public DemarcacaoPostgressContextConfig() : base()
        {
            var path = Path.GetDirectoryName(this.GetType().Assembly.Location);
            SetModelStore(new DefaultDbModelStore(path));
        }
    }
}
