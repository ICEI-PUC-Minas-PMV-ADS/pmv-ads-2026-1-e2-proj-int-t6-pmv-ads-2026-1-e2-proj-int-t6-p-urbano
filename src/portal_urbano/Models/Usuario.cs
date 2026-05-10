using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoUrbano.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [NotMapped]
        [Required]
        public string Senha { get; set; } = string.Empty;

        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        public string? Telefone { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.Now;

        [StringLength(120)]
        public string? ResetToken { get; set; }

        public DateTime? ResetTokenExpiraEm { get; set; }

        public List<Denuncia> Denuncias { get; set; } = new();
    }
}
