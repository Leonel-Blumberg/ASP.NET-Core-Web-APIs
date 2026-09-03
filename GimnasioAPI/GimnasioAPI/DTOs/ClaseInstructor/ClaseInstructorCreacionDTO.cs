using System.ComponentModel.DataAnnotations;
using GimnasioAPI.Entidades;

namespace GimnasioAPI.DTOs.ClaseInstructor
{
    public class ClaseInstructorCreacionDTO
    {
        public int InstructorId { get; set; }
        [EnumDataType(typeof(RolEnum), ErrorMessage = "El rol indicado no es válido.")]
        public RolEnum Rol { get; set; }
    }
}
