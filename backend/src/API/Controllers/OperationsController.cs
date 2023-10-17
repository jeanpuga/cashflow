using API.Filter;
using APPLICATION.Features.Operations.Models;
using APPLICATION.Features.Operations.Models.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class OperationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OperationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Method by filter type report [Today, Consolidate, Balance]
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Return the Operation list</response>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return NotFound();
            }

            try
            {
                var result = await _mediator.Send(request.Mapper(User), cancellationToken);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Method create transaction operation
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return NotFound();
            }

            Log.Information("[OperationsController]Create - {@payload}", request);

            try
            {
                await _mediator.Send(request.Mapper(User), cancellationToken);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}