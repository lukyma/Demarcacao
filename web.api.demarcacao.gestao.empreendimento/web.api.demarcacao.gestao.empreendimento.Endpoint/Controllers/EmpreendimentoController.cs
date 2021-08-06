using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using patterns.strategy;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.gestao.empreendimento.Endpoint.Config;
using web.api.demarcacao.gestao.empreendimento.Endpoint.Models;
using web.api.demarcacao.gestao.empreendimento.Endpoint.Models.HandleValidaiton;
using web.api.demarcacao.gestao.empreendimento.Endpoint.Models.Reseponse;
using web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy;
using web.api.demarcacao.gestao.empreendimento.Service.Application.Strategy.Request;
using static web.api.demarcacao.gestao.empreendimento.Endpoint.Config.Swagger.EmpreendimentoExampleMessage;

namespace web.api.demarcacao.gestao.empreendimento.Endpoint.Controllers
{
    [Authorize]
    public class EmpreendimentoController : BaseApiController
    {
        public EmpreendimentoController(IMapper mapper,
                                        IStrategyContext strategyContext,
                                        IHandleValidation handleValidation) : base(mapper, strategyContext, handleValidation)
        {
        }

        /// <summary>
        /// Cadastro inicial de um empreendimento.
        /// </summary>
        /// <param name="empreendimento">Body empreendimento</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status201Created, SwaggerConstants.Descricao201, typeof(EmpreendimentoVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(EmpreendimentoRequestMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpPost("v{version:apiVersion}/[controller]")]
        [Authorize("CadEmp")]
        public async Task<IActionResult> PostAsync([FromBody] EmpreendimentoVM empreendimento, CancellationToken cancellationToken)
        {
            var request = Mapper.Map<CadastraEmpreendimentoRequest>(empreendimento);
            await StrategyContext.HandlerAsync<CadastraEmpreendimentoRequest, DefaultResponse>(request, cancellationToken);
            return await ApiResponseAsync(Created("api/v1/empreendimento/1", empreendimento));
        }

        /// <summary>
        /// Listagem de todos os empreendimentos
        /// </summary>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status200OK, SwaggerConstants.Descricao200, typeof(ListaEmpreendimentoResponseVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpGet("v{version:apiVersion}/[controller]")]
        [Authorize("ListEmp")]
        public async Task<IActionResult> GetAsync([FromQuery] int pagina,
                                                  [FromServices] IDistributedCache cache,
                                                  CancellationToken cancellationToken)
        {
            string valorJSON = await cache.GetStringAsync($"ListaEmpreendimento-page-{pagina}");
            if (valorJSON != null)
            {
                return await ApiResponseAsync(Ok(JsonConvert.DeserializeObject<ListaEmpreendimentoResponseVM>(valorJSON)));
            }

            ListaEmpreendimentoQueryResponse response = await StrategyContext
                .HandlerAsync<ListaEmpreendimentoQuery,
                              ListaEmpreendimentoQueryResponse>(new ListaEmpreendimentoQuery() { Pagina = pagina }, cancellationToken);
            if (response == null || !response.Itens.Any())
            {
                return await ApiResponseAsync(NotFound());
            }

            await cache.SetStringAsync($"ListaEmpreendimento-page-{pagina}",
                                       JsonConvert.SerializeObject(response),
                                       new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3) }, cancellationToken);

            return await ApiResponseAsync(Ok(Mapper.Map<ListaEmpreendimentoResponseVM>(response)));
        }

        /// <summary>
        /// Exibe os detalhes de um empreendimento específico. 
        /// </summary>
        /// <param name="id">identificador do empreendimento</param>
        /// <param name="cancellationToken">identificador do empreendimento</param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status200OK, SwaggerConstants.Descricao200, typeof(EmpreendimentoResponseVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(EmpreendimentoGetIdMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpGet("v{version:apiVersion}/[controller]/{id:int}")]
        [Authorize("ListEmp")]
        public async Task<IActionResult> GetByIdAsync(int id, 
                                                      [FromServices] IDistributedCache cache, 
                                                      CancellationToken cancellationToken)
        {
            string valorJSON = await cache.GetStringAsync($"Empreendimento-{id}");
            if (valorJSON != null)
            {
                return await ApiResponseAsync(Ok(JsonConvert.DeserializeObject<EmpreendimentoResponseVM>(valorJSON)));
            }

            var response = Mapper.Map<EmpreendimentoResponseVM>(await StrategyContext.HandlerAsync<RetornaEmpreendimentoQuery,
                                                        RetornarEmpreendimentoQueryResponse>(new RetornaEmpreendimentoQuery(id), cancellationToken));
            if (response == null)
            {
                return await ApiResponseAsync(NotFound());
            }

            await cache.SetStringAsync($"Empreendimento-{id}",
                                       JsonConvert.SerializeObject(response),
                                       new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3) }, cancellationToken);

            return await ApiResponseAsync(Ok(response));
        }

        /// <summary>
        /// Atualiza as informações de um empreendimento.
        /// </summary>
        /// <param name="id">identificador do empreendimento</param>
        /// <param name="empreendimento">recurso empreendimento</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status204NoContent, SwaggerConstants.Descricao204)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(EmpreendimentoRequestMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpPut("v{version:apiVersion}/[controller]/{id:int}")]
        [Authorize("AtualEmp")]
        public async Task<IActionResult> PutAsync(int id, 
                                                  [FromBody] EmpreendimentoVM empreendimento,
                                                  CancellationToken cancellationToken)
        {
            var request = Mapper.Map<AtualizaEmpreendimentoRequest>(empreendimento);
            request.IdEmpreendimento = id;
            var response = await StrategyContext.HandlerAsync<AtualizaEmpreendimentoRequest, DefaultResponse>(request, cancellationToken);
            if (!response.IsNotDefault)
            {
                return await ApiResponseAsync(NotFound());
            }

            return await ApiResponseAsync(NoContent());
        }


        /// <summary>
        /// Excluí um empreendimento.
        /// </summary>
        /// <param name="id">identificador do empreendimento</param>
        /// <returns></returns>
        [ApiVersion("1")]
        [SwaggerResponse(StatusCodes.Status204NoContent, SwaggerConstants.Descricao204)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, SwaggerConstants.Descricao400, type: typeof(ErrorMessage))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(EmpreendimentoDeleteMessage))]
        [SwaggerResponse(StatusCodes.Status404NotFound, SwaggerConstants.Descricao404)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, SwaggerConstants.Descricao500)]
        [HttpDelete("v{version:apiVersion}/[controller]/{id:int}")]
        [Authorize("ExcEmp")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await Task.FromResult(NoContent());
        }
    }
}
