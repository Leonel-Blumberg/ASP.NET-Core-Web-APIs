using GimnasioAPI.DTOs.ClaseInstructor;

namespace GimnasioAPI.DTOs.Instructor
{
    public class InstructorConClasesDTO : InstructorDTO
    {
        public List<InstructorClaseDTO> Clases { get; set; } = [];
    }
}
