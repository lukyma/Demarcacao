using pattern.strategy;
using System.Linq;

namespace web.api.demarcacao.gestao.usuarios.Endpoint.Models.HandleValidaiton
{
    public class HandleValidation : IHandleValidation
    {
        private IValidationErrors ValidationFailures { get; }
        public HandleValidation(IValidationErrors validationFailures)
        {
            ValidationFailures = validationFailures;
        }

        public ErrorMessage GetAllMessages()
        {
            return new ErrorMessage(ValidationFailures.Select(o => new Error(o.ErrorCode, o.ErrorMessage)));
        }

        public bool HasErroMessage()
        {
            return ValidationFailures.Any();
        }
    }
}
