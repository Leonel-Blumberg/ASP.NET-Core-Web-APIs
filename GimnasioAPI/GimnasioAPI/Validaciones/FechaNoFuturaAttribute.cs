using System.ComponentModel.DataAnnotations;

namespace GimnasioAPI2.Validaciones
{
    public class FechaNoFuturaAttribute : ValidationAttribute
    {
        public FechaNoFuturaAttribute()
        {
            ErrorMessage = "El campo {0} no puede ser una fecha futura.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            if (value is not DateTime fecha)
                return new ValidationResult("El campo debe ser una fecha.");

            if (fecha > DateTime.UtcNow)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}
