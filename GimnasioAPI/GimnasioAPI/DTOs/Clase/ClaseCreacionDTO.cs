using GimnasioAPI.DTOs.ClaseInstructor;

namespace GimnasioAPI.DTOs.Clase
{
    public class ClaseCreacionDTO : ClasePatchDTO
    {
        public List<ClaseInstructorCreacionDTO> Instructores { get; set; } = [];
    }
}
