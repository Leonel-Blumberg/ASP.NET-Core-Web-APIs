using TareasAPI.Entidades;

namespace TareasAPI.Interfaces
{
    public interface IRepositorioNotas
    {
        IReadOnlyCollection<Nota> ObtenerNotas();
        IReadOnlyCollection<Nota> ObtenerNotasPorTarea(int tareaId);
        Nota? ObtenerNotaPorId(int id);
        Nota Agregar(Nota nota);
        bool Modificar(int id, Nota nota);
        bool Eliminar(int id);
    }
}
