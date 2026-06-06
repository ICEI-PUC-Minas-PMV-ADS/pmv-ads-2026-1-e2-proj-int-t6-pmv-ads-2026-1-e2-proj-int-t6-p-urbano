using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace portal_urbano.Services
{
    public sealed class ModeracaoIaResult
    {
        public bool ViolaRegras { get; init; }
        public string? Motivo { get; init; }
    }

    public class GeminiModeracaoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GeminiModeracaoService> _logger;

        public GeminiModeracaoService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<GeminiModeracaoService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Novo contexto a cada chamada: envia titulo e descricao para analise isolada.
        /// Retorna null se a API nao estiver configurada ou falhar (use fallback local).
        /// </summary>
        public async Task<ModeracaoIaResult?> AnalisarTituloDescricaoAsync(
            string? titulo,
            string? descricao,
            CancellationToken cancellationToken = default)
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(titulo) && string.IsNullOrWhiteSpace(descricao))
            {
                return new ModeracaoIaResult { ViolaRegras = false };
            }

            var model = _configuration["Gemini:Model"] ?? "gemini-2.5-flash";
            var prompt = MontarPromptModeracao(titulo ?? string.Empty, descricao ?? string.Empty);

            try
            {
                var url =
                    $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

                var requestBody = new GeminiGenerateRequest(
                    new[]
                    {
                        new GeminiContent(
                            "user",
                            new[] { new GeminiPart(prompt) })
                    });

                var json = JsonSerializer.Serialize(requestBody, JsonOptions);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var response = await _httpClient.PostAsync(url, content, cancellationToken);
                var responseText = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning(
                        "Gemini moderacao falhou: {Status} {Body}",
                        response.StatusCode,
                        responseText);
                    return null;
                }

                using var document = JsonDocument.Parse(responseText);
                var root = document.RootElement;
                if (!root.TryGetProperty("candidates", out var candidatesProp) || candidatesProp.ValueKind != JsonValueKind.Array)
                {
                    return null;
                }

                var firstCandidate = candidatesProp.EnumerateArray().FirstOrDefault();
                if (firstCandidate.ValueKind != JsonValueKind.Object)
                {
                    return null;
                }

                if (!firstCandidate.TryGetProperty("content", out var contentProp) || contentProp.ValueKind != JsonValueKind.Object)
                {
                    return null;
                }

                if (!contentProp.TryGetProperty("parts", out var partsProp) || partsProp.ValueKind != JsonValueKind.Array)
                {
                    return null;
                }

                var firstPart = partsProp.EnumerateArray().FirstOrDefault();
                var textoResposta = firstPart.GetProperty("text").GetString();

                if (string.IsNullOrWhiteSpace(textoResposta))
                {
                    return null;
                }

                if (!TryInterpretarRespostaModeracao(textoResposta, out var viola, out var motivo))
                {
                    _logger.LogWarning("Resposta da IA fora do formato esperado: {Resposta}", textoResposta);
                    return null;
                }

                return new ModeracaoIaResult
                {
                    ViolaRegras = viola,
                    Motivo = motivo
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar Gemini para moderacao");
                return null;
            }
        }

        private static string MontarPromptModeracao(string titulo, string descricao)
        {
            return
                "Voce e um moderador de conteudo do Portal Urbano, plataforma civica de denuncias urbanas " +
                "(buracos, iluminacao, lixo, seguranca, etc.).\n\n" +
                "Analise APENAS o titulo e a descricao abaixo quanto a:\n" +
                "- Linguagem ofensiva, xingamentos ou discurso de odio\n" +
                "- Spam, golpes, cassino, pornografia ou links suspeitos\n" +
                "- Ameacas, incitacao a violencia ou conteudo perigoso\n\n" +
                "Conteudo legitimo sobre problemas urbanos NAO deve ser bloqueado.\n\n" +
                $"Titulo:\n{titulo}\n\n" +
                $"Descricao:\n{descricao}\n\n" +
                "Responda SOMENTE com um JSON valido, sem markdown e sem texto extra:\n" +
                "{\"viola\":false,\"motivo\":\"\"}\n\n" +
                "Use \"viola\": true somente se houver violacao clara das regras acima.\n" +
                "Quando viola for true, preencha \"motivo\" com uma frase curta em portugues explicando o problema.";
        }

        private static bool TryInterpretarRespostaModeracao(string texto, out bool viola, out string? motivo)
        {
            viola = false;
            motivo = null;

            var json = texto.Trim();
            var inicio = json.IndexOf('{');
            var fim = json.LastIndexOf('}');
            if (inicio < 0 || fim <= inicio)
            {
                return false;
            }

            json = json.Substring(inicio, fim - inicio + 1);

            try
            {
                using var doc = JsonDocument.Parse(json);
                if (!doc.RootElement.TryGetProperty("viola", out var propViola))
                {
                    return false;
                }

                viola = propViola.ValueKind switch
                {
                    JsonValueKind.True => true,
                    JsonValueKind.False => false,
                    JsonValueKind.String => bool.TryParse(propViola.GetString(), out var b) && b,
                    _ => false
                };

                if (doc.RootElement.TryGetProperty("motivo", out var propMotivo))
                {
                    motivo = propMotivo.GetString();
                }

                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private sealed record GeminiGenerateRequest(GeminiContent[] Contents);

        private sealed record GeminiContent(string Role, GeminiPart[] Parts);

        private sealed record GeminiPart(string Text);
    }
}
