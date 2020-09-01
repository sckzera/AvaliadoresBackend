using System;
using System.Linq;
using AutoMapper;
using avaliacao_backend.Api.Models;
using avaliacao_backend.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace avaliacao_backend.Api.Controllers
{
    [ApiController]
    [Route("avaliacoes")]
    public class AvaliacaoController : ControllerBase
    {
        private const string _mensagemErroExcecao = "Ocorreu um erro inesperado";
        private readonly ILogger<AvaliacaoController> _logger;
        private readonly IMapper _mapper;
        private readonly IAvaliacaoRepository _repository;

        public AvaliacaoController(ILogger<AvaliacaoController> logger
            , IAvaliacaoRepository repository
            , IMapper mapper)
        {
            _logger = logger;

            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Cria uma nova avaliação
        /// </summary>
        /// <param name="avaliacao">Representa os dados da avaliacao</param>
        /// <param name="codigoAvaliador">Representa o codigo do avaliador</param>
        /// <param name="codigoResumo">Representa o codigo do resumo</param>
        /// <response code="201">Retorna quando um recurso foi criado com sucesso</response>
        /// <response code="400">Retorna quando houve uma requisição mal formada</response>
        /// <response code="409">Retorna quando o recurso ja existe</response>
        /// <response code="500">Retorna quando houve um erro interno do serviço</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroRetorno))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErroRetorno))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErroRetorno))]
        [Produces("application/json")]
        public IActionResult Create(AvaliacaoInclusao avaliacao, [FromHeader] Guid codigoAvaliador, [FromHeader] Guid codigoResumo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(new ErroRetorno(string.Join(", ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))));
                }

                if (_repository.Existe(codigoAvaliador, codigoResumo))
                {
                    return new ConflictObjectResult(new ErroRetorno("A avaliação ja foi realizada. Codigo da avaliacao: " + _repository.Codigo(codigoAvaliador, codigoResumo)));
                }

                var avaliacaoEntity = _mapper.Map<Entities.Avaliacao>(avaliacao);

                avaliacaoEntity.CodigoAvaliador = codigoAvaliador;
                avaliacaoEntity.CodigoResumo = codigoResumo;

                _repository.Incluir(avaliacaoEntity);

                if (_repository.Salvar())
                {
                    return CreatedAtRoute(null,null);
                }

                return new JsonResult(500, new ErroRetorno(_mensagemErroExcecao));
            }
            catch (Exception ex)
            {
                _logger.LogError(_mensagemErroExcecao, ex);
                return new JsonResult(500, new ErroRetorno(_mensagemErroExcecao));
            }
        }

        /// <summary>
        /// Altera os dados da avaliacao
        /// </summary>
        /// <response code="201">Retorna quando um recurso foi criado com sucesso</response>
        /// <response code="400">Retorna quando houve uma requisição mal formada</response>
        /// <response code="409">Retorna quando o recurso ja existe</response>
        /// <response code="500">Retorna quando houve um erro interno do serviço</response>
        [HttpPut("{codigo}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErroRetorno))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErroRetorno))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErroRetorno))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErroRetorno))]
        [Produces("application/json")]
        public IActionResult Put(Guid codigo, AvaliacaoAlteracao avaliacaoAlteracao)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new BadRequestObjectResult(new ErroRetorno(string.Join(", ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))));
                }

                var avaliacaoEntity = _mapper.Map<avaliacao_backend.Api.Entities.Avaliacao>(avaliacaoAlteracao);

                avaliacaoEntity.Codigo = codigo;

                _repository.Alterar(avaliacaoEntity);

                if (_repository.Salvar())
                {
                    return NoContent();
                }

                return new JsonResult(500, new ErroRetorno(_mensagemErroExcecao));
            }
            catch (Exception ex)
            {
                _logger.LogError(_mensagemErroExcecao, ex);
                return new JsonResult(500, new ErroRetorno(_mensagemErroExcecao));
            }
        }

    }
}