using GimnasioAPI.Entidades;

namespace GimnasioAPI.DTOs.ClaseInstructor
{
    public class InstructorClaseDTO
    {
        public int ClaseId { get; set; }
        public required string NombreClase { get; set; }
        public RolEnum Rol { get; set; }
    }
}
