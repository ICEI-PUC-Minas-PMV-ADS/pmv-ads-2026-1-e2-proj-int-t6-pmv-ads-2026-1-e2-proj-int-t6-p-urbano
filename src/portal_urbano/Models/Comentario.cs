using System.ComponentModel.DataAnnotations;

namespace ProjetoUrbano.Models
{
    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }

        public int IdDenuncia { get; set; }
        public int IdUsuario { get; set; }

        public string Texto { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        public Denuncia? Denuncia { get; set; }
        public Usuario? Usuario { get; set; }
    }
}