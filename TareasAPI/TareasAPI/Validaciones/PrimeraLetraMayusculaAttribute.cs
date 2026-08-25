using System.ComponentModel.DataAnnotations;

namespace TareasAPI.Validaciones
{
    public class PrimeraLetraMayusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? texto = value?.ToString();

            if (string.IsNullOrEmpty(texto))
                return ValidationResult.Success;

            if (!char.IsUpper(texto[0]))
                return new ValidationResult($"El campo {validationContext.DisplayName} debe empezar con mayúscula.");

            return ValidationResult.Success;
        }
    }
}
