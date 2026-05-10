using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Options;

namespace ProjetoUrbano.Services.Email
{
    public class SendGridEmailService : IEmailService
    {
        private readonly SendGridOptions _options;

        public SendGridEmailService(IOptions<SendGridOptions> options)
        {
            _options = options.Value;
        }

        public async Task EnviarAsync(string destino, string assunto, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                throw new InvalidOperationException("Configure a ApiKey do SendGrid no appsettings.json.");
            }

            var client = new SendGridClient(_options.ApiKey);
            var from = new EmailAddress(_options.FromEmail, _options.FromName);
            var to = new EmailAddress(destino);

            var msg = MailHelper.CreateSingleEmail(from, to, assunto, mensagem, mensagem);
            await client.SendEmailAsync(msg);
        }
    }
}
