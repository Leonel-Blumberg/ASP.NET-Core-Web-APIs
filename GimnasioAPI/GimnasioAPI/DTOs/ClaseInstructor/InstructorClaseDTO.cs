using GimnasioAPI2.Entidades;

namespace GimnasioAPI2.DTOs.ClaseInstructor
{
    public class InstructorClaseDTO
    {
        public int ClaseId { get; set; }
        public required string NombreClase { get; set; }
        public RolEnum Rol { get; set; }
    }
}
