using GimnasioAPI.DTOs.Resena;
using GimnasioAPI.Interfaces;

namespace GimnasioAPI.Repositorios
{
    public class RepositorioResenasLista : IRepositorioResenas
    {
        private readonly Lock candado = new();
        private readonly List<ResenaDTO> resenaDTOs = [];

        public IReadOnlyCollection<ResenaDTO> ObtenerPorClase(int claseId)
        {
            lock (candado)
                return [.. resenaDTOs.Where(x => x.ClaseId == claseId)];
        }

        public ResenaDTO Crear(int claseId, ResenaCreacionDTO resenaCreacionDTO)
        {
            lock(candado)
            {
                ResenaDTO resenaDTO = new()
                {
                    Id = Guid.NewGuid(),
                    ClaseId = claseId,
                    Texto = resenaCreacionDTO.Texto,
                    Puntaje = resenaCreacionDTO.Puntaje,
                    FechaPublicacion = DateTime.UtcNow
                };

                resenaDTOs.Add(resenaDTO);
                return resenaDTO;
            }

        }

    }
}
