namespace portal_urbano.Models;

public class PerfilDenunciaItem
{
    public int IdDenuncia { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }
    public int TotalLikes { get; set; }
    public int TotalComentarios { get; set; }
    public string? ImagemUrl { get; set; }
    public string Endereco { get; set; } = string.Empty;
    public bool Anonima { get; set; }
}
