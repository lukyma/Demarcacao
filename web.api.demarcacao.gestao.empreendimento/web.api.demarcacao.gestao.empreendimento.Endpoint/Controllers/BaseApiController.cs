using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using patterns.strategy;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.empreendimento.Endpoint.Models.HandleValidaiton;

namespace web.api.demarcacao.gestao.empreendimento.Endpoint.Controllers
{
    [Route("api")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IStrategyContext StrategyContext { get; }
        protected IMapper Mapper { get; }
        protected IHandleValidation HandleValidation { get; }
        protected BaseApiController(IMapper mapper, IStrategyContext strategyContext, IHandleValidation handleValidation)
        {
            Mapper = mapper;
            StrategyContext = strategyContext;
            HandleValidation = handleValidation;
        }

        protected async Task<IActionResult> ApiResponseAsync(IActionResult actionResult)
        {
            if (HandleValidation.HasErroMessage())
            {
                return await Task.FromResult(BadRequest(HandleValidation.GetAllMessages()));
            }
            else
            {
                return actionResult;
            }
        }
    }
}
