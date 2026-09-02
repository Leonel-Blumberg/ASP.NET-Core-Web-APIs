using System.ComponentModel.DataAnnotations;
using GimnasioAPI2.Validaciones;

namespace GimnasioAPI2.DTOs.Disciplina
{
    public class DisciplinaCreacionDTO
    {
        [Required]
        [PrimeraLetraMayuscula]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public required string Nombre { get; set; }
    }
}
