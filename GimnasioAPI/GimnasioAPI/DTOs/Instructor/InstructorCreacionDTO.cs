using System.ComponentModel.DataAnnotations;
using GimnasioAPI.Validaciones;

namespace GimnasioAPI.DTOs.Instructor
{
    public class InstructorCreacionDTO
    {
        [Required]
        [PrimeraLetraMayuscula]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public required string Nombres { get; set; }
        [Required]
        [PrimeraLetraMayuscula]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public required string Apellidos { get; set; }  
    }
}
