using System;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}