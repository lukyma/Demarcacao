namespace web.api.demarcacao.gestao.terreno.Endpoint.Models.HandleValidaiton
{
    public interface IHandleValidation
    {
        bool HasErroMessage();
        ErrorMessage GetAllMessages();
    }
}
