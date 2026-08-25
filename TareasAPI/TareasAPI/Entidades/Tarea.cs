using System.ComponentModel.DataAnnotations;
using TareasAPI.Validaciones;

namespace TareasAPI.Entidades
{
    public class Tarea : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos.")]
        [PrimeraLetraMayuscula]
        public required string Titulo { get; set; }
        public required DateOnly FechaInicio { get; set; }
        public required DateOnly FechaFin { get; set; }
        public bool Completada { get; set; } = false;

        public int ListaId { get; set; }
        public Lista? Lista { get; set; }

        // Validaciones
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FechaFin < FechaInicio)
                yield return new ValidationResult("La fecha de fin de la tarea no puede ser anterior a la fecha de inicio.", [nameof(FechaInicio), nameof(FechaFin)]);
        }
    }
}
