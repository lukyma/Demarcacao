using System.Collections.Generic;
using System.Linq;

namespace web.api.demarcacao.gestao.usuarios.Endpoint.Models
{
    public class ErrorMessage
    {
        public ErrorMessage(IEnumerable<Error> errors)
        {
            Errors = errors.OrderBy(o => o.Code);
        }
        public IEnumerable<Error> Errors { get; set; }
    }

    public class Error
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
