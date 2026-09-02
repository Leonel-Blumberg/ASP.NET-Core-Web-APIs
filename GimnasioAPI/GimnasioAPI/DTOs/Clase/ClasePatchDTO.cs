using System.ComponentModel.DataAnnotations;
using GimnasioAPI2.Validaciones;

namespace GimnasioAPI2.DTOs.Clase
{
    public class ClasePatchDTO : IValidatableObject
    {
        [Required]
        [PrimeraLetraMayuscula]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public required string Nombre { get; set; }
        [Range(0, int.MaxValue)]
        public int CupoMinimo { get; set; }
        [Range(0, int.MaxValue)]
        public int CupoMaximo { get; set; }
        [Range(0, int.MaxValue)]
        public int DuracionMinutos { get; set; }
        public bool Activa { get; set; } = false;

        public int DisciplinaId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CupoMinimo > CupoMaximo)
                yield return new ValidationResult("El cupo minimo no puede ser mayor que el cupo máximo.");
        }
    }
}
