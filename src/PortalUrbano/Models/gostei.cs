using System.ComponentModel.DataAnnotations;

namespace portal_urbano.Models
{
    public class Gostei
    {
        [Key]
        public long LikeId { get; set; }

        public int UsuarioId { get; set; }
        public int DenunciaId { get; set; }

        public DateTime? CriadoEm { get; set; }
    }
}
