namespace GimnasioAPI.Entidades
{
    public class ClaseInstructor
    {
        public int ClaseId { get; set; }
        public int InstructorId { get; set; }
        public RolEnum Rol { get; set; }
        public Clase? Clase { get; set; }
        public Instructor? Instructor { get; set; }
    }

    public enum RolEnum
    {
        Titular = 1,
        Suplente = 2
    }
}
