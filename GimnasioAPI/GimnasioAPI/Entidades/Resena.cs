namespace GimnasioAPI2.Entidades
{
    public class Resena
    {
        public Guid Id { get; set; }
        public int ClaseId { get; set; }
        public required string Texto { get; set; }
        public int Puntaje { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
