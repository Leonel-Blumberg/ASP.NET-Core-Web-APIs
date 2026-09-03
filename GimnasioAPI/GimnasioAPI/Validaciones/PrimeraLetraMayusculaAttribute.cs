using System.ComponentModel.DataAnnotations;

namespace GimnasioAPI.Validaciones
{
    public class PrimeraLetraMayusculaAttribute : ValidationAttribute
    {
        public PrimeraLetraMayusculaAttribute()
        {
            ErrorMessage = "El campo {0} debe comenzar con una letra Mayúscula.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? texto = value?.ToString();

            if (string.IsNullOrEmpty(texto))
                return ValidationResult.Success;

            if (!char.IsUpper(texto[0]))
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}
