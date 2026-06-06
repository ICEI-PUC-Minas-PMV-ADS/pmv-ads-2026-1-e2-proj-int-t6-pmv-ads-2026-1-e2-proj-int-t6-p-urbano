using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace portal_urbano.Services.Email
{
    public class SendGridValidationHostedService : IHostedService
    {
        private readonly ILogger<SendGridValidationHostedService> _logger;
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _config;

        public SendGridValidationHostedService(ILogger<SendGridValidationHostedService> logger, IHttpClientFactory httpFactory, IConfiguration config)
        {
            _logger = logger;
            _httpFactory = httpFactory;
            _config = config;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var apiKey = _config["SendGrid:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                _logger.LogWarning("SendGrid API key não configurada.");
                return;
            }

            try
            {
                var source = "appsettings";
                var shortKey = apiKey.Length > 10 ? apiKey[..10] + "..." : apiKey;
                _logger.LogInformation("Validando SendGrid API key (source={Source}, key={KeyPreview})", source, shortKey);
                var client = _httpFactory.CreateClient("SendGridHealth");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
                var resp = await client.GetAsync("https://api.sendgrid.com/v3/user/profile", cancellationToken);
                if (resp.IsSuccessStatusCode)
                {
                    _logger.LogInformation("SendGrid API key validada com sucesso.");
                }
                else
                {
                    var body = await resp.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogWarning("SendGrid validação falhou: {Status} {Body}", resp.StatusCode, body);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning(ex, "Erro ao validar SendGrid API key");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
