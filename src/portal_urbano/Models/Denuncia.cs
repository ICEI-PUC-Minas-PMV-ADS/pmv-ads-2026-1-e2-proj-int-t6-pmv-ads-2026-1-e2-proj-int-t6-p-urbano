using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoUrbano.Models
{
    public class Denuncia
    {
        [Key]
        public int IdDenuncia { get; set; }

        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        public int IdCategoria { get; set; }
        public Categoria? Categoria { get; set; }

        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? ImagemUrl { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public string Rua { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string? Complemento { get; set; }
        public string? Status { get; set; }
        public int StatusAnonimo { get; set; } = 0;

        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public List<Gostei> Likes { get; set; } = new List<Gostei>();

        public DateTime CriadoEm { get; set; }

        public List<Reporte> Reportes { get; set; } = new();
    }
}