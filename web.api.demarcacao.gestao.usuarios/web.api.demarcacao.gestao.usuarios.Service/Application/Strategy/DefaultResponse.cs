using System;
using System.Diagnostics.CodeAnalysis;

namespace web.api.demarcacao.gestao.usuarios.Service.Application.Strategy.Request
{
    public struct DefaultResponse : IEquatable<DefaultResponse>
    {
        /// <summary>
        /// Validate DefaultResponse struct is not default (default(DefaultResponse)).
        /// </summary>
        public bool IsNotDefault { get; set; }
        public DefaultResponse(bool isNotDefault)
        {
            IsNotDefault = isNotDefault;
        }
        public bool Equals([AllowNull] DefaultResponse other)
        {
            return base.Equals(other);
        }
    }
}
