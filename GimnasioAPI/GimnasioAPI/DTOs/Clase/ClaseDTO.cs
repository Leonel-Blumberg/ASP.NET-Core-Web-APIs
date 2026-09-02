namespace GimnasioAPI2.DTOs.Clase
{
    public class ClaseDTO
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public int CupoMinimo { get; set; }
        public int CupoMaximo { get; set; }
        public int DuracionMinutos { get; set; }
        public bool Activa { get; set; }
        public int DisciplinaId { get; set; }
    }
}
