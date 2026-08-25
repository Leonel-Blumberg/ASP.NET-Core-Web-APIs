using TareasAPI.Entidades;
using TareasAPI.Interfaces;

namespace TareasAPI.Repositorios
{
    public class RepositorioNotasEnMemoria : IRepositorioNotas
    {
        private readonly Lock candado = new();
        private readonly List<Nota> listaNotas = [];
        private int siguienteId = 1;

        public IReadOnlyCollection<Nota> ObtenerNotas()
        {
            lock (candado)
                return [.. listaNotas];
        }

        public Nota? ObtenerNotaPorId(int id)
        {
            lock (candado)
                return listaNotas.FirstOrDefault(x => x.Id == id);
        }

        public IReadOnlyCollection<Nota> ObtenerNotasPorTarea(int tareaId)
        {
            lock (candado)
                return [.. listaNotas.Where(x => x.TareaId == tareaId)];
        }

        public Nota Agregar(Nota nota)
        {
            lock (candado)
            {
                nota.Id = siguienteId;
                siguienteId++;

                listaNotas.Add(nota);
                return nota;
            }
        }

        public bool Modificar(int id, Nota nota)
        {
            lock (candado)
            {
                int indice = listaNotas.FindIndex(x => x.Id == id);

                if (indice == -1)
                    return false;

                listaNotas[indice] = nota;
                return true;
            }
        }

        public bool Eliminar(int id)
        {
            lock (candado)
                return listaNotas.RemoveAll(x => x.Id == id) > 0;
        }
    }
}
