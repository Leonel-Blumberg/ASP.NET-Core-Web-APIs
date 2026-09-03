using GimnasioAPI.DTOs.ClaseInstructor;
using GimnasioAPI.DTOs.Disciplina;

namespace GimnasioAPI.DTOs.Clase
{
    public class ClaseConHijosDTO : ClaseDTO
    {
        public DisciplinaDTO? Disciplina { get; set; }
        public List<ClaseInstructorDTO> Instructores { get; set; } = [];
    }
}
