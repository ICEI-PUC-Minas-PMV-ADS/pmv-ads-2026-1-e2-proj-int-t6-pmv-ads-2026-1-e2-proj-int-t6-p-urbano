using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace portal_urbano.Models
{
    public class Reporte
    {
        [Key]
        public int IdReporte { get; set; }

        public int IdDenuncia { get; set; }
        public int IdUsuario { get; set; }

        public string Motivo { get; set; }
        [Column("detalhes")]
        public string? Detalhes { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;

        public Denuncia? Denuncia { get; set; }
        public Usuario? Usuario { get; set; }
    }
}