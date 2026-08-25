using RecetasAPI.Entidades;

namespace RecetasAPI.Interfaces
{
    public interface IRepositorioComentarios
    {
        IReadOnlyCollection<Comentario> GetComentarios();
        Comentario? GetComentarioPorId(int id);
        IReadOnlyCollection<Comentario> GetComentariosPorTexto(string texto);
        Comentario PostComentario(Comentario comentario);
        bool PutComentario(int id, Comentario comentario);
        bool DeleteComentario(int id);
    }
}
