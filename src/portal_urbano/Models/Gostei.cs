using System.ComponentModel.DataAnnotations;

namespace ProjetoUrbano.Models
{
    public class Gostei
    {
        [Key]
        public int LikeId { get; set; }

        public int UsuarioId { get; set; }
        public int DenunciaId { get; set; }

        public DateTime? CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
