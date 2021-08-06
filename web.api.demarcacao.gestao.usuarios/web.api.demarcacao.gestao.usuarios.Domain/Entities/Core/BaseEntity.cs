using System;
using System.Runtime.Serialization;

namespace web.api.demarcacao.gestao.usuarios.Domain.Entities.Core
{
    public interface IBaseEntity<TEntityId>
    {
        TEntityId Id { get; set; }
    }
    public abstract class BaseEntity<TEntityId> : IBaseEntity<TEntityId>
    {
        private TEntityId _Id { get; set; }
        public TEntityId Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (!(value is long) && !(value is Guid) && !(value is int) && !(value is short))
                {
                    throw new PropertyTypeException(@"A propriedade ""Id"" deve ser do tipo: int, Guid ou long short");
                }

                _Id = value;
            }
        }
    }

    [Serializable]
    public class PropertyTypeException : Exception
    {

        public PropertyTypeException()
        {
        }

        public PropertyTypeException(string message)
            : base(message)
        {
        }

        public PropertyTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PropertyTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

}
