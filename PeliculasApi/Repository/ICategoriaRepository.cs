using PeliculasApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeliculasApi.Repository
{
    public interface ICategoriaRepository
    {
        Task<ICollection<Categoria>> GetCategorias();
        Task<Categoria> GetCategoria(int categoriaId);
        Task<bool> ExisteCategoria(string nombre);
        Task<bool> ExisteCategoria(int id);
        Task<bool> ExisteCategoria(int id, string nombre);
        Task<bool> CrearCategoria(Categoria categoria);
        Task<bool> ActualizarCategoria(Categoria categoria);
        Task<bool> BorrarCategoria(int id);
        Task<bool> Guardar();
    }
}