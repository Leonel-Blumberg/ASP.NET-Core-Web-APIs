using System.ComponentModel.DataAnnotations;

namespace GimnasioAPI.DTOs.Resena
{
    public class ResenaCreacionDTO
    {
        [Required]
        [MaxLength(250, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public required string Texto { get; set; }
        [Range(0, 10)]
        public int Puntaje { get; set; }
    }
}
