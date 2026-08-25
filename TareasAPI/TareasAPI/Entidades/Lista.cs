using System.ComponentModel.DataAnnotations;
using TareasAPI.Validaciones;

namespace TareasAPI.Entidades
{
    public class Lista
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos.")]
        [PrimeraLetraMayuscula]
        public required string Nombre { get; set; }
        public List<Tarea> Tareas { get; set; } = [];
    }
}
