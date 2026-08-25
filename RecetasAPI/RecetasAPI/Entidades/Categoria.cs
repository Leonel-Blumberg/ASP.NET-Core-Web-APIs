using RecetasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace RecetasAPI.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [PrimeraLetraMayuscula]
        public required string Nombre { get; set; }
        public List<Receta> Receta { get; set; } = [];
    }
}
