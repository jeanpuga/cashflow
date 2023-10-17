using APPLICATION.Shared.Domain.Interfaces;
using APPLICATION.Shared.Domain.Options;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace APPLICATION.Shared.ExternalServices
{
    public class SeqlogHealthServices : ISeqlogHealthServices
    {
        private readonly IHttpClientFactory _factory;
        private readonly LogOptions _logOptions;

        public SeqlogHealthServices(IHttpClientFactory factory, IOptions<LogOptions> logOptions)
        {
            _factory = factory;
            _logOptions = logOptions.Value;
        }

        public async Task<bool> GetSeqlogHealthcheck(CancellationToken cancellationToken)
        {
            var httpClient = _factory.CreateClient("GetSeqlogHealthcheck");
            var response = await httpClient.GetAsync("", cancellationToken);
            return response.IsSuccessStatusCode;
        }
    }
}