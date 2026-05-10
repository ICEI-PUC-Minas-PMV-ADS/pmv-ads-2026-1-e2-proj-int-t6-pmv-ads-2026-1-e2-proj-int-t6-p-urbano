namespace ProjetoUrbano.Services.Email
{
    public interface IEmailService
    {
        Task EnviarAsync(string destino, string assunto, string mensagem);
    }
}
