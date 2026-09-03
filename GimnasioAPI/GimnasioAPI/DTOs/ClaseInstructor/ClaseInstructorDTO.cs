using GimnasioAPI.Entidades;

namespace GimnasioAPI.DTOs.ClaseInstructor
{
    public class ClaseInstructorDTO
    {
        public int InstructorId { get; set; }
        public required string NombreCompleto { get; set; }
        public RolEnum Rol { get; set; }
    }
}
