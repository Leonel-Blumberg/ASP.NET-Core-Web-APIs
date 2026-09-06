using GimnasioAPI.DTOs.Resena;
using GimnasioAPI.Entidades;
using GimnasioAPI.Interfaces;

namespace GimnasioAPI.Repositorios
{
    public class RepositorioResenaDiccionario : IRepositorioResenas
    {
        private readonly Lock candado = new();
        private readonly Dictionary<int, List<Resena>> resenasPorClase = [];

        public IReadOnlyCollection<Resena> ObtenerPorClase(int claseId)
        {
            lock (candado)
                return resenasPorClase.TryGetValue(claseId, out List<Resena>? resenas) ? resenas.ToList() : [];
        }

        public Resena Crear(int claseId, ResenaCreacionDTO resenaCreacionDTO)
        {
            lock (candado)
            {
                Resena resena = new()
                {
                    Id = Guid.NewGuid(),
                    ClaseId = claseId,
                    Texto = resenaCreacionDTO.Texto,
                    Puntaje = resenaCreacionDTO.Puntaje,
                    FechaPublicacion = DateTime.UtcNow
                };

                if (!resenasPorClase.ContainsKey(claseId))
                    resenasPorClase[claseId] = [];

                resenasPorClase[claseId].Add(resena);
                return resena;
            }
        }

    }
}
