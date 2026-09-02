using GimnasioAPI2.DTOs.ClaseInstructor;

namespace GimnasioAPI2.DTOs.Clase
{
    public class ClaseCreacionDTO : ClasePatchDTO
    {
        public List<ClaseInstructorCreacionDTO> Instructores { get; set; } = [];
    }
}
