namespace GimnasioAPI2.Entidades
{
    public class Instructor
    {
        public int Id { get; set; }
        public required string Nombres { get; set; }
        public required string Apellidos { get; set; }
        public List<ClaseInstructor> Clases { get; set; } = [];
    }
}
