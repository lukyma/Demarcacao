using System;

namespace web.api.demarcacao.gestao.usuarios.Domain.Exceptions
{
    public class CoordenadasRangeException : Exception
    {
        public CoordenadasRangeException(short tamanhoMinimoLista) : base($"A lista deve ter ao menos {tamanhoMinimoLista} posições.")
        {

        }
    }
}
