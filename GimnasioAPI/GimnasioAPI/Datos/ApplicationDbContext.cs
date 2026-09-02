using GimnasioAPI2.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GimnasioAPI2.Datos
{
    public class ApplicationDbContext(DbContextOptions opciones) : DbContext(opciones)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Clase>().Property(x => x.Nombre).HasMaxLength(50);

            modelBuilder.Entity<ClaseInstructor>().HasKey(x => new { x.ClaseId, x.InstructorId });

            modelBuilder.Entity<Disciplina>().Property(x => x.Nombre).HasMaxLength(50);

            modelBuilder.Entity<Instructor>().Property(x => x.Nombres).HasMaxLength(50);
            modelBuilder.Entity<Instructor>().Property(x => x.Apellidos).HasMaxLength(50);
        }

        public DbSet<Clase> Clases { get; set; }
        public DbSet<ClaseInstructor> ClasesInstructores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Instructor> Instructores { get; set; }

    }
}
