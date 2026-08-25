using System.ComponentModel.DataAnnotations;
using TareasAPI.Validaciones;

namespace TareasAPI.Entidades
{
    public class Nota
    {
        public int Id { get; set; }

        public int TareaId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos.")]
        [PrimeraLetraMayuscula]
        public required string Texto { get; set; }
    }
}
