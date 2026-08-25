using Microsoft.EntityFrameworkCore;
using TareasAPI.Entidades;

namespace TareasAPI.Datos
{
    public class ApplicationDbContext(DbContextOptions opciones) : DbContext(opciones)
    {
        public DbSet<Lista> Listas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
    }
}
