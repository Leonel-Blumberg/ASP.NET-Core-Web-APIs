namespace GimnasioAPI.Entidades
{
    public class Disciplina
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public List<Clase> Clases { get; set; } = [];
    }
}
