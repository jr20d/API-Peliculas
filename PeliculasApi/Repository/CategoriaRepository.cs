using Microsoft.EntityFrameworkCore;
using PeliculasApi.Data;
using PeliculasApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly PeliculasDbContetx contetx;

        public CategoriaRepository(PeliculasDbContetx contetx)
        {
            this.contetx = contetx;
        }

        public async Task<bool> ActualizarCategoria(Categoria categoria)
        {
            contetx.Categorias.Update(categoria);
            return await Guardar();
        }

        public async Task<bool> BorrarCategoria(int id)
        {
            var categoria = contetx.Categorias.Find(id);
            if (categoria != null)
            {
                contetx.Categorias.Remove(categoria);
                return await Guardar();
            }
            return false;
        }

        public async Task<bool> CrearCategoria(Categoria categoria)
        {
            contetx.Categorias.Add(categoria);
            return await Guardar();
        }

        public async Task<bool> ExisteCategoria(string nombre)
        {
            return await contetx.Categorias.AnyAsync(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }

        public async Task<bool> ExisteCategoria(int id)
        {
            return await contetx.Categorias.AnyAsync(c => c.CategoriaId == id);
        }

        public async Task<bool> ExisteCategoria(int id, string nombre)
        {
            return await contetx.Categorias.AnyAsync(c => c.CategoriaId != id && c.Nombre == nombre);
        }

        public async Task<Categoria> GetCategoria(int categoriaId)
        {
            return await contetx.Categorias.FindAsync(categoriaId);
        }

        public async Task<ICollection<Categoria>> GetCategorias()
        {
            return await contetx.Categorias.OrderBy(c => c.Nombre).ToListAsync();
        }

        public async Task<bool> Guardar()
        {
            return await contetx.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
