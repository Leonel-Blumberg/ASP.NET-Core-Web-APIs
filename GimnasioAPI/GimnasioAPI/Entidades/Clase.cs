namespace GimnasioAPI.Entidades
{
    public class Clase
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public int CupoMinimo { get; set; }
        public int CupoMaximo { get; set; }
        public int DuracionMinutos { get; set; }
        public bool Activa { get; set; } = false;

        public int DisciplinaId { get; set; }
        public Disciplina? Disciplina { get; set; }
        public List<ClaseInstructor> Instructores { get; set; } = [];
    }
}
