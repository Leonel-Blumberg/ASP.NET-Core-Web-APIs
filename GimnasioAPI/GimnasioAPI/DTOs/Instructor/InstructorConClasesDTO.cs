using GimnasioAPI2.DTOs.ClaseInstructor;

namespace GimnasioAPI2.DTOs.Instructor
{
    public class InstructorConClasesDTO : InstructorDTO
    {
        public List<InstructorClaseDTO> Clases { get; set; } = [];
    }
}
