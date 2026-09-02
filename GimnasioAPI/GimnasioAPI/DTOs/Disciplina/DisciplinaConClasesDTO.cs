using GimnasioAPI2.DTOs.Clase;

namespace GimnasioAPI2.DTOs.Disciplina
{
    public class DisciplinaConClasesDTO : DisciplinaDTO
    {
        public List<ClaseDTO> Clases { get; set; } = [];
    }
}
