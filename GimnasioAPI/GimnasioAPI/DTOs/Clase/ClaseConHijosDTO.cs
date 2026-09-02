using GimnasioAPI2.DTOs.ClaseInstructor;
using GimnasioAPI2.DTOs.Disciplina;

namespace GimnasioAPI2.DTOs.Clase
{
    public class ClaseConHijosDTO : ClaseDTO
    {
        public DisciplinaDTO? Disciplina { get; set; }
        public List<ClaseInstructorDTO> Instructores { get; set; } = [];
    }
}
