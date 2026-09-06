using GimnasioAPI.DTOs.Resena;

namespace GimnasioAPI.Interfaces
{
    public interface IRepositorioResenas
    {
        IReadOnlyCollection<ResenaDTO> ObtenerPorClase(int claseId);
        ResenaDTO Crear(int claseId, ResenaCreacionDTO resenaCreacionDTO);
    }
}
