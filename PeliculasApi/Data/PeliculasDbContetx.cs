using Microsoft.EntityFrameworkCore;
using PeliculasApi.Models;

namespace PeliculasApi.Data
{
    public class PeliculasDbContetx : DbContext
    {
        public PeliculasDbContetx(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
    }
}
