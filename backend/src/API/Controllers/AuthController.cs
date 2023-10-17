using APPLICATION.Features.Auth.Models;
using APPLICATION.Shared.Domain.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using static API.Customize.StartupExtensions;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConn _connectionString;
        private readonly RabbitmqOptions _rabbitmqOptions;
        private readonly LogOptions _logOptions;

        public AuthController(IMediator mediator, IConn connectionString, IOptions<RabbitmqOptions> rabbitmqOptions, IOptions<LogOptions> logOptions)
        {
            _mediator = mediator;
            _connectionString = connectionString;
            _rabbitmqOptions = rabbitmqOptions.Value;
            _logOptions = logOptions.Value;
        }

        /// <summary>
        /// Login a specific Auth methods
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Return payload with token</response>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return NotFound();
            }

            try
            {
                var result = await _mediator.Send(request, cancellationToken);

                if (result == null)
                {
                    return Unauthorized();
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}