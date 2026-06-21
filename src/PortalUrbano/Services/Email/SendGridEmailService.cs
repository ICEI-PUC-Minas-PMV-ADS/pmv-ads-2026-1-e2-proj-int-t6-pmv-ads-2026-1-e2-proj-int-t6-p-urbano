using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace portal_urbano.Services.Email;

public class SendGridEmailService : IEmailService
{
    private readonly SendGridOptions _options;
    private readonly ILogger<SendGridEmailService> _logger;
    private readonly SendGridClient? _client;

    public SendGridEmailService(IOptions<SendGridOptions> options, ILogger<SendGridEmailService> logger)
    {
        _options = options.Value;
        _logger = logger;

        if (string.IsNullOrWhiteSpace(_options.ApiKey))
        {
            _logger.LogWarning("SendGrid:ApiKey não configurada. E-mails de recuperação de senha não serão enviados.");
            return;
        }

        _client = new SendGridClient(_options.ApiKey);
    }

    public Task SendAsync(string to, string subject, string html) =>
        SendAsync(to, subject, html, plainText: null);

    public async Task SendPasswordResetAsync(string to, string nome, string resetUrl)
    {
        var displayName = string.IsNullOrWhiteSpace(nome) ? "usuário" : nome;
        var subject = "Redefinição de senha - Portal Urbano";
        var html = $"""
            <div style="font-family:Arial,sans-serif;max-width:560px;margin:0 auto;color:#0f172a">
              <h2 style="color:#f97316">Portal Urbano</h2>
              <p>Olá, {System.Net.WebUtility.HtmlEncode(displayName)}!</p>
              <p>Recebemos uma solicitação para redefinir sua senha. Clique no botão abaixo para criar uma nova senha. O link expira em 2 horas.</p>
              <p style="margin:28px 0">
                <a href="{resetUrl}" style="background:#f97316;color:#071027;padding:12px 20px;border-radius:8px;text-decoration:none;font-weight:bold">
                  Redefinir minha senha
                </a>
              </p>
              <p style="color:#64748b;font-size:14px">Se o botão não funcionar, copie e cole este link no navegador:<br/>{resetUrl}</p>
              <p style="color:#64748b;font-size:14px">Se você não solicitou esta alteração, ignore este e-mail.</p>
            </div>
            """;
        var plainText = $"Olá, {displayName}!\n\nRedefina sua senha acessando: {resetUrl}\n\nO link expira em 2 horas. Se você não solicitou, ignore este e-mail.";

        await SendAsync(to, subject, html, plainText);
    }

    private async Task SendAsync(string to, string subject, string html, string? plainText)
    {
        if (_client == null)
            throw new InvalidOperationException("SendGrid não está configurado. Defina SendGrid:ApiKey em User Secrets ou variáveis de ambiente.");

        if (string.IsNullOrWhiteSpace(_options.FromEmail))
            throw new InvalidOperationException("SendGrid:FromEmail não configurado.");

        var msg = new SendGridMessage();
        msg.SetFrom(_options.FromEmail, _options.FromName);
        msg.AddTo(to);
        msg.SetSubject(subject);
        msg.AddContent("text/html", html);
        if (!string.IsNullOrWhiteSpace(plainText))
            msg.AddContent("text/plain", plainText);

        var response = await _client.SendEmailAsync(msg);
        var body = await response.Body.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(
                "SendGrid rejeitou o envio para {Email}. Status={Status} Body={Body}",
                to, response.StatusCode, body);
            throw new InvalidOperationException(
                $"Falha ao enviar e-mail (HTTP {(int)response.StatusCode}). Verifique remetente verificado no painel SendGrid.");
        }

        _logger.LogInformation("E-mail enviado com sucesso para {Email}", to);
    }
}
