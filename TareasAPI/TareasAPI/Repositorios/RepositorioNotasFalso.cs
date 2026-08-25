using TareasAPI.Entidades;
using TareasAPI.Interfaces;

namespace TareasAPI.Repositorios
{
    public class RepositorioNotasFalso : IRepositorioNotas
    {
        private readonly List<Nota> notas =
        [
            new() { Id = 1, TareaId = 1, Texto = "Nota falsa 1" },
            new() { Id = 2, TareaId = 1, Texto = "Nota falsa 2" },
        ];

        public Nota Agregar(Nota nota)
        {
            return notas[0];
        }

        public bool Eliminar(int id)
        {
            return true;
        }

        public bool Modificar(int id, Nota nota)
        {
            return true;
        }

        public IReadOnlyCollection<Nota> ObtenerNotas()
        {
            return [.. notas];
        }
        public Nota? ObtenerNotaPorId(int id)
        {
            return notas[1];
        }

        public IReadOnlyCollection<Nota> ObtenerNotasPorTarea(int tareaId)
        {
            return [.. notas];
        }
    }
}
