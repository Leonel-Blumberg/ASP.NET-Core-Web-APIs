using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasAPI.Datos;
using TareasAPI.Entidades;
using TareasAPI.Interfaces;

namespace TareasAPI.Controllers
{
    [ApiController]
    [Route("api/notas")]
    public class NotasController(ApplicationDbContext contexto, IRepositorioNotas repositorio, ILogger<NotasController> logger) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Nota> GetNotas()
        {
            logger.LogInformation("Obteniendo el listado de notas.");
            return repositorio.ObtenerNotas();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Nota> GetPorId([FromRoute] int id)
        {
            Nota? nota = repositorio.ObtenerNotaPorId(id);

            if (nota is null)
            {
                logger.LogWarning("No se encontró la nota de id {Id} al intentar buscarla.", id);
                return NotFound();
            }

            return nota;
        }

        [HttpGet("nota-tarea/{tareaId:int}")]
        public IEnumerable<Nota> GetPorTarea([FromRoute] int tareaId)
        {
            return repositorio.ObtenerNotasPorTarea(tareaId);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Nota nota)
        {
            bool existeTarea = await contexto.Tareas.AnyAsync(x => x.Id == nota.TareaId);

            if (!existeTarea)
            {
                ModelState.AddModelError(nameof(nota.TareaId), $"La tarea de id {nota.TareaId} no existe.");
                return ValidationProblem();
            }

            Nota notaCreada;

            try
            {
                notaCreada = repositorio.Agregar(nota);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar la nota {Texto}.", nota.Texto);
                return StatusCode(500, "No se pudo guardar la nota.");
            }

            return CreatedAtAction(nameof(GetPorId), new { id = notaCreada.Id }, notaCreada);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Nota nota)
        {
            if (id != nota.Id)
            {
                ModelState.AddModelError(nameof(nota.Id), "Las ids deben coincidir.");
                return ValidationProblem();
            }

            bool existeTarea = await contexto.Tareas.AnyAsync(x => x.Id == nota.TareaId);

            if (!existeTarea)
            {
                ModelState.AddModelError(nameof(nota.TareaId), $"La tarea de id {nota.TareaId} no existe.");
                return ValidationProblem();
            }

            bool notaActualizada = repositorio.Modificar(id, nota);

            if (!notaActualizada)
            {
                logger.LogWarning("No se encontró la nota de id {Id} al intentar actualizarla.", id);
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (!repositorio.Eliminar(id))
            {
                logger.LogWarning("No se encontró la nota de id {Id} al intentar borrarla.", id);
                return NotFound();
            }

            return Ok();
        }
    }
}
