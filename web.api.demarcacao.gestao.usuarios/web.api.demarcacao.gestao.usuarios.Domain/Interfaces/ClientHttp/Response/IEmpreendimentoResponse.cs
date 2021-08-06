using System;
using System.Collections.Generic;
using System.Text;

namespace web.api.demarcacao.gestao.usuarios.Domain.Interfaces.ClientHttp
{
    public interface IEmpreendimentoResponse
    {
        long Id { get; set; }
        string Descricao { get; set; }
    }
}
