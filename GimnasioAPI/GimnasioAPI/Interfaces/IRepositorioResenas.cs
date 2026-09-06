using GimnasioAPI.DTOs.Resena;
using GimnasioAPI.Entidades;

namespace GimnasioAPI.Interfaces
{
    public interface IRepositorioResenas
    {
        IReadOnlyCollection<Resena> ObtenerPorClase(int claseId);
        Resena Crear(int claseId, ResenaCreacionDTO resenaCreacionDTO);
    }
}
