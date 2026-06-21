namespace portal_urbano.Services.Email;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string html);
    Task SendPasswordResetAsync(string to, string nome, string resetUrl);
}
