using RecetasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace RecetasAPI.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public int RecetaId { get; set; }

        [Required]
        [StringLength(120, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [PrimeraLetraMayuscula]
        public required string Texto { get; set; }
    }
}
