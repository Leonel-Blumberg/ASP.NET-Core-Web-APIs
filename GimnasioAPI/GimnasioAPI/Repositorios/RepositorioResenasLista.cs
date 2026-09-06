using GimnasioAPI.DTOs.Resena;
using GimnasioAPI.Entidades;
using GimnasioAPI.Interfaces;

namespace GimnasioAPI.Repositorios
{
    public class RepositorioResenasLista : IRepositorioResenas
    {
        private readonly Lock candado = new();
        private readonly List<Resena> resenas = [];

        public IReadOnlyCollection<Resena> ObtenerPorClase(int claseId)
        {
            lock (candado)
                return [.. resenas.Where(x => x.ClaseId == claseId)];
        }

        public Resena Crear(int claseId, ResenaCreacionDTO resenaCreacionDTO)
        {
            lock(candado)
            {
                Resena resena = new()
                {
                    Id = Guid.NewGuid(),
                    ClaseId = claseId,
                    Texto = resenaCreacionDTO.Texto,
                    Puntaje = resenaCreacionDTO.Puntaje,
                    FechaPublicacion = DateTime.UtcNow
                };

                resenas.Add(resena);
                return resena;
            }

        }

    }
}
