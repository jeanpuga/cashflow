using APPLICATION.Shared.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IHealthRepository _healthRepository;
        private readonly IRabbitmqHealthServices _rabbitmqHealthServices;
        private readonly ISeqlogHealthServices _seqlogHealthServices;

        public HealthController(IHealthRepository healthRepository, IRabbitmqHealthServices rabbitmqHealthServices, ISeqlogHealthServices seqlogHealthServices)
        {
            _healthRepository = healthRepository;
            _rabbitmqHealthServices = rabbitmqHealthServices;
            _seqlogHealthServices = seqlogHealthServices;
        }

        [HttpGet]
        [Route("rabbitmq")]
        public async Task<IActionResult> RabbitMqHealth(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _rabbitmqHealthServices.GetRabbitMqHealthcheck(cancellationToken);

                if (result)
                {
                    return Ok(new HealthCheckResult(
                           status: HealthStatus.Healthy,
                           description: "API OK")
                           );
                }
                else
                {
                    return Ok(new HealthCheckResult(
                       status: HealthStatus.Unhealthy,
                       description: "API Unhealthy")
                       );
                }
            }
            catch (Exception ex)
            {
                return Ok(new HealthCheckResult(
                          status: HealthStatus.Degraded,
                          description: "API it is out")
                          );
            }
        }

        [HttpGet]
        [Route("seqlog")]
        public async Task<IActionResult> SeqLogHealth(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _seqlogHealthServices.GetSeqlogHealthcheck(cancellationToken);

                if (result)
                {
                    return Ok(new HealthCheckResult(
                           status: HealthStatus.Healthy,
                           description: "API OK")
                           );
                }
                else
                {
                    return Ok(new HealthCheckResult(
                       status: HealthStatus.Unhealthy,
                       description: "API Unhealthy")
                       );
                }
            }
            catch (Exception ex)
            {
                return Ok(new HealthCheckResult(
                          status: HealthStatus.Degraded,
                          description: "API it is out")
                          );
            }
        }

        [HttpGet]
        [Route("sqlserverlogs")]
        public async Task<IActionResult> SQLServerLogsHealth(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _healthRepository.SelectOne(cancellationToken);

                if (result)
                {
                    return Ok(new HealthCheckResult(
                           status: HealthStatus.Healthy,
                           description: "API OK")
                           );
                }
                else
                {
                    return Ok(new HealthCheckResult(
                       status: HealthStatus.Unhealthy,
                       description: "API Unhealthy")
                       );
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    return Ok(new HealthCheckResult(
                      status: HealthStatus.Unhealthy,
                      description: "API Unhealthy")
                      );
                }
                else
                {
                    return Ok(new HealthCheckResult(
                              status: HealthStatus.Degraded,
                              description: "API it is out")
                              );
                }
            }
        }

        [HttpGet]
        [Route("sqlserver")]
        public async Task<IActionResult> SQLServerHealth(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _healthRepository.SelectOne(cancellationToken);

                if (result)
                {
                    return Ok(new HealthCheckResult(
                           status: HealthStatus.Healthy,
                           description: "API OK")
                           );
                }
                else
                {
                    return Ok(new HealthCheckResult(
                       status: HealthStatus.Unhealthy,
                       description: "API Unhealthy")
                       );
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Sequence contains no elements")
                {
                    return Ok(new HealthCheckResult(
                      status: HealthStatus.Unhealthy,
                      description: "API Unhealthy")
                      );
                }
                else
                {
                    return Ok(new HealthCheckResult(
                              status: HealthStatus.Degraded,
                              description: "API it is out")
                              );
                }
            }
        }
    }
}