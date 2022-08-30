using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.Data;
using PeliculasApi.Models;
using PeliculasApi.Models.DTO;
using PeliculasApi.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeliculasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository categoriaRepository;
        private readonly IMapper mapper;

        public CategoriasController(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            this.categoriaRepository = categoriaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategorias()
        {
            var categorias = await categoriaRepository.GetCategorias();

            return Ok(mapper.Map<ICollection<CategoriaDto>>(categorias));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoria(int id)
        {
            var categoria = await categoriaRepository.GetCategoria(id);
            if (categoria == null)
            {
                return NotFound(new {
                    Mensaje = "No se encontró ninguna categoría con ese ID"
                });
            }
            return Ok(mapper.Map<CategoriaDto>(categoria));
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategoria([FromBody()] CategoriaDto categoria)
        {
            if (await categoriaRepository.ExisteCategoria(categoria.Nombre))
            {
                return Conflict(new {
                    Mensaje = "Ya existe una categoría con ese nombre"
                });
            }
            var nuevaCategoria = mapper.Map<Categoria>(categoria);
            if (await categoriaRepository.CrearCategoria(nuevaCategoria))
            {
                return Created($"/api/categorias/{nuevaCategoria.CategoriaId}", nuevaCategoria);
            }
            
            return Conflict(new { Mensaje = "No se pudo crear la categoría. Intentelo mas tarde" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoria([FromBody()] Categoria categoriaDto, int id)
        {
            var categoria = mapper.Map<Categoria>(categoriaDto);
            categoria.CategoriaId = id;
            if (!await categoriaRepository.ExisteCategoria(id))
            {
                return NotFound(new { Mensaje = "La categoría no existe" });
            }
            if (await categoriaRepository.ExisteCategoria(id, categoria.Nombre))
            {
                return Conflict(new { Mensaje = "Ya existe otra categoría con ese nombre" });
            }
            if (await categoriaRepository.ActualizarCategoria(categoria))
            {
                return Ok(new { Mensaje = "Los datos de la categoría fueron actualizados" });
            }
            return Conflict(new { Mensaje = "Los datos de la categoría no fueron actualizados" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoria(int id)
        {
            if (!await categoriaRepository.ExisteCategoria(id))
            {
                return NotFound(new { Mensaje = "La categoría no existe" });
            }
            if (await categoriaRepository.BorrarCategoria(id))
            {
                return Ok(new { Mensaje = "La categoría ha sido eliminada" });
            }
            return Conflict(new { Mensaje = "La categoría no pudo ser eliminada" });
        }
    }
}
