using System.ComponentModel.DataAnnotations;

namespace portal_urbano.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Icone { get; set; } = string.Empty;

        public List<Denuncia> Denuncias { get; set; } = new();
    }
}