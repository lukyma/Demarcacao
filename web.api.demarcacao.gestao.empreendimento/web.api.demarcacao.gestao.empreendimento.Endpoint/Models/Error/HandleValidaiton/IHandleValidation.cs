namespace web.api.demarcacao.gestao.empreendimento.Endpoint.Models.HandleValidaiton
{
    public interface IHandleValidation
    {
        bool HasErroMessage();
        ErrorMessage GetAllMessages();
    }
}
