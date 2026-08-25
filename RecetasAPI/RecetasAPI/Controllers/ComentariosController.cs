using Microsoft.AspNetCore.Mvc;
using RecetasAPI.Entidades;
using RecetasAPI.Interfaces;

namespace RecetasAPI.Controllers
{
    [ApiController]
    [Route("api/comentarios")]
    public class ComentariosController(IRepositorioComentarios comentarios, ILogger<ComentariosController> logger) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Comentario> GetComentarios()
        {
            logger.LogInformation("Obteniendo el listado de comentarios.");
            return comentarios.GetComentarios();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Comentario> GetComentarioPorId([FromRoute] int id)
        {
            Comentario? comentario = comentarios.GetComentarioPorId(id);

            if (comentario is null)
            {
                logger.LogWarning("No se encontró el comentario de id {Id} al intentar buscarlo.", id);
                return NotFound();
            }

            return comentario;
        }

        [HttpGet("{texto:regex(^[[a-zA-Z ]]+$)}")]
        public IEnumerable<Comentario> GetComentariosPorTexto([FromRoute] string texto) => comentarios.GetComentariosPorTexto(texto);

        [HttpPost]
        public ActionResult PostComentario([FromBody] Comentario comentario)
        {
            Comentario comentarioCreado;

            try
            {
                comentarioCreado = comentarios.PostComentario(comentario);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar el comentario {Texto}.", comentario.Texto);
                return StatusCode(500, "No se pudo guardar el comentario.");
            }

            return CreatedAtAction(nameof(GetComentarioPorId), new { id = comentarioCreado.Id }, comentarioCreado);
        }

        [HttpPut("{id:int}")]
        public ActionResult PutComentario([FromRoute] int id, [FromBody] Comentario comentario)
        {
            if (id != comentario.Id)
            {
                ModelState.AddModelError(nameof(comentario.Id), "Las ids deben coincidir.");
                return ValidationProblem();
            }

            if (!comentarios.PutComentario(id, comentario))
            {
                logger.LogWarning("No se encontró el comentario de id {Id} al intentar actualizarlo.", id);
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteComentario([FromRoute] int id)
        {
            if (!comentarios.DeleteComentario(id))
            {
                logger.LogWarning("No se encontró el comentario de id {Id} al intentar eliminarlo.", id);
                return NotFound();
            }

            return Ok();
        }
    }
}
