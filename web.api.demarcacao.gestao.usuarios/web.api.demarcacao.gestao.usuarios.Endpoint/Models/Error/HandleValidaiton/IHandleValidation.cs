namespace web.api.demarcacao.gestao.usuarios.Endpoint.Models.HandleValidaiton
{
    public interface IHandleValidation
    {
        bool HasErroMessage();
        ErrorMessage GetAllMessages();
    }
}
