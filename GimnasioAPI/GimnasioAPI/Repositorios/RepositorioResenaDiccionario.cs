using GimnasioAPI.DTOs.Resena;
using GimnasioAPI.Interfaces;

namespace GimnasioAPI.Repositorios
{
    public class RepositorioResenaDiccionario : IRepositorioResenas
    {
        private readonly Lock candado = new();
        private readonly Dictionary<int, List<ResenaDTO>> resenaDTOs = [];

        public IReadOnlyCollection<ResenaDTO> ObtenerPorClase(int claseId)
        {
            lock (candado)
                return resenaDTOs.TryGetValue(claseId, out List<ResenaDTO>? resenas) ? resenas.ToList() : [];
        }

        public ResenaDTO Crear(int claseId, ResenaCreacionDTO resenaCreacionDTO)
        {
            lock (candado)
            {
                ResenaDTO resenaDTO = new()
                {
                    Id = Guid.NewGuid(),
                    ClaseId = claseId,
                    Texto = resenaCreacionDTO.Texto,
                    Puntaje = resenaCreacionDTO.Puntaje,
                    FechaPublicacion = DateTime.UtcNow
                };

                if (!resenaDTOs.ContainsKey(claseId))
                    resenaDTOs[claseId] = [];

                resenaDTOs[claseId].Add(resenaDTO);
                return resenaDTO;
            }
        }

    }
}
