using Microsoft.AspNetCore.Http.HttpResults;
using RecetasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace RecetasAPI.Entidades
{
    public class Receta : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        [PrimeraLetraMayuscula]
        public required string Titulo { get; set; }

        [Range(1, int.MaxValue)]
        public int TiempoTotalMinutos { get; set; }

        [Range(0, int.MaxValue)]
        public int TiempoCoccionMinutos { get; set; }
        public bool Vegetariana { get; set; } = false;

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TiempoTotalMinutos < TiempoCoccionMinutos)
                yield return new ValidationResult("El tiempo de coccion no puede ser mayor que el tiempo total.");
        }
    }
}
