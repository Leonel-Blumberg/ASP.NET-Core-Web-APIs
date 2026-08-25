using RecetasAPI.Entidades;
using RecetasAPI.Interfaces;

namespace RecetasAPI.Repositorios
{
    public class RepositorioComentariosEnMemoria : IRepositorioComentarios
    {
        private readonly Lock candado = new();
        private readonly List<Comentario> comentarios = [];
        private int siguienteId = 1;

        public IReadOnlyCollection<Comentario> GetComentarios()
        {
            lock (candado)
                return [.. comentarios];
        }

        public Comentario? GetComentarioPorId(int id)
        {
            lock (candado)
                return comentarios.FirstOrDefault(x => x.Id == id);
        }

        public IReadOnlyCollection<Comentario> GetComentariosPorTexto(string texto)
        {
            lock (candado)
                return [.. comentarios.Where(x => x.Texto.Contains(texto))];
        }

        public Comentario PostComentario(Comentario comentario)
        {
            lock (candado)
            {
                comentario.Id = siguienteId;
                siguienteId++;

                comentarios.Add(comentario);
                return comentario;
            }
        }

        public bool PutComentario(int id, Comentario comentario)
        {
            lock (candado)
            {
                int indice = comentarios.FindIndex(x => x.Id == id);

                if (indice == -1)
                    return false;

                comentarios[indice] = comentario;
                return true;
            }
        }

        public bool DeleteComentario(int id)
        {
            lock (candado)
                return comentarios.RemoveAll(x => x.Id == id) > 0;
        }
    }
}
