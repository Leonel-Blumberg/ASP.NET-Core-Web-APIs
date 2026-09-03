using GimnasioAPI.DTOs.Clase;

namespace GimnasioAPI.DTOs.Disciplina
{
    public class DisciplinaConClasesDTO : DisciplinaDTO
    {
        public List<ClaseDTO> Clases { get; set; } = [];
    }
}
