using APPLICATION.Shared.Domain.Interfaces;
using APPLICATION.Shared.Domain.Options;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Shared.ExternalServices
{
    public class RabbitmqHealthServices : IRabbitmqHealthServices
    {
        private readonly IHttpClientFactory _factory;
        private readonly RabbitmqOptions _rabbitmqOptions;

        public RabbitmqHealthServices(IHttpClientFactory factory, IOptions<RabbitmqOptions> rabbitmqOptions)
        {
            _factory = factory;
            _rabbitmqOptions = rabbitmqOptions.Value;
        }

        public async Task<bool> GetRabbitMqHealthcheck(CancellationToken cancellationToken)
        {
            var httpClient = _factory.CreateClient("GetRabbitMqHealthcheck");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_rabbitmqOptions.User}:{_rabbitmqOptions.Password}")));
            var response = await httpClient.GetAsync($"{_rabbitmqOptions.Url}/api/vhosts", cancellationToken);
            return response.IsSuccessStatusCode;
        }
    }
}