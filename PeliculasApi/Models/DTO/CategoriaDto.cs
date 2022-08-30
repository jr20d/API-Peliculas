using System;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Models.DTO
{
    public class CategoriaDto
    {
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}