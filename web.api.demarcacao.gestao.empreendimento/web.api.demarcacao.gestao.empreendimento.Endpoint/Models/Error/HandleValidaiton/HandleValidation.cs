using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace web.api.demarcacao.gestao.empreendimento.Endpoint.Models.HandleValidaiton
{
    public class HandleValidation : IHandleValidation
    {
        private IList<ValidationFailure> ValidationFailures { get; }
        public HandleValidation(IList<ValidationFailure> validationFailures)
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
