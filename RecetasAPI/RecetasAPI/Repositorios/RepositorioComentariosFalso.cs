using RecetasAPI.Entidades;
using RecetasAPI.Interfaces;

namespace RecetasAPI.Repositorios
{
    public class RepositorioComentariosFalso : IRepositorioComentarios
    {
        private readonly List<Comentario> comentarios = 
        [
            new() { Id = 1, RecetaId = 1, Texto = "La receta es sencilla."},
            new() { Id = 2, RecetaId = 2, Texto = "La receta es complicada."}
        ];

        public IReadOnlyCollection<Comentario> GetComentarios() => comentarios;

        public Comentario? GetComentarioPorId(int id) => comentarios.FirstOrDefault();

        public IReadOnlyCollection<Comentario> GetComentariosPorTexto(string texto) => comentarios;

        public Comentario PostComentario(Comentario comentario) => comentario;

        public bool PutComentario(int id, Comentario comentario) => false;

        public bool DeleteComentario(int id) => true;
    }
}
