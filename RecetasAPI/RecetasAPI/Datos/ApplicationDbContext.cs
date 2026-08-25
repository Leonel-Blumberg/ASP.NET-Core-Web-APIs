using Microsoft.EntityFrameworkCore;
using RecetasAPI.Entidades;

namespace RecetasAPI.Datos
{
    public class ApplicationDbContext(DbContextOptions opciones) : DbContext(opciones)
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Receta> Recetas { get; set; }
    }
}
