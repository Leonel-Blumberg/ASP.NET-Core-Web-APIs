using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasAPI.Datos;
using TareasAPI.Entidades;

namespace TareasAPI.Controllers
{
    [ApiController]
    [Route("api/tareas")]
    public class TareasController(ApplicationDbContext contexto, ILogger<TareasController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Tarea>> Get([FromQuery] bool? completadas, [FromQuery] int? listaId)
        {
            logger.LogInformation("Obteniendo el listado de tareas.");

            IQueryable<Tarea> consulta = contexto.Tareas;

            if (completadas is not null)
                consulta = consulta.Where(x => x.Completada == completadas.Value);

            if (listaId is not null)
                consulta = consulta.Where(x => x.ListaId == listaId.Value);

            return await consulta.Include(x => x.Lista).ToListAsync();
        }

        [HttpGet("{id:int}", Name = "ObtenerTareaPorId")]
        public async Task<ActionResult<Tarea>> Get([FromRoute] int id, [FromHeader(Name = "incluir-lista")] bool? incluirLista)
        {
            IQueryable<Tarea> consulta = contexto.Tareas;

            if (incluirLista == true)
                consulta = consulta.Include(x => x.Lista);

            Tarea? tarea = await consulta.FirstOrDefaultAsync(x => x.Id == id);

            if (tarea is null)
            {
                logger.LogWarning("No se encontró la tarea de id {Id} al intentar buscarla.", id);
                return NotFound();
            }

            return tarea;
        }

        [HttpGet("{titulo:alpha}")]
        public async Task<IEnumerable<Tarea>> Get([FromRoute] string titulo, [FromHeader(Name = "incluir-lista")] bool? incluirLista)
        {
            IQueryable<Tarea> consulta = contexto.Tareas;

            if (incluirLista == true)
                consulta = consulta.Include(x => x.Lista);

            return await consulta.Where(x => x.Titulo.Contains(titulo)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Tarea tarea)
        {
            bool existeLista = await contexto.Listas.AnyAsync(x => x.Id == tarea.ListaId);

            if (!existeLista)
            {
                ModelState.AddModelError(nameof(tarea.ListaId), $"La lista de id {tarea.ListaId} no existe.");
                return ValidationProblem();
            }

            try
            {
                contexto.Tareas.Add(tarea);
                await contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar la tarea {Titulo}.", tarea.Titulo);
                return StatusCode(500, "No se pudo guardar la tarea.");
            }

            return CreatedAtRoute("ObtenerTareaPorId", new { id = tarea.Id }, tarea);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Tarea tarea)
        {
            if (id != tarea.Id)
            {
                ModelState.AddModelError(nameof(tarea.Id), "Las ids deben coincidir.");
                return ValidationProblem();
            }

            bool existeTarea = await contexto.Tareas.AnyAsync(x => x.Id == id);

            if (!existeTarea)
            {
                logger.LogWarning("No se encontró la tarea de id {Id} al intentar actualizarla.", id);
                return NotFound();
            }

            bool existeLista = await contexto.Listas.AnyAsync(x => x.Id == tarea.ListaId);

            if (!existeLista)
            {
                ModelState.AddModelError(nameof(tarea.ListaId), $"La lista de id {tarea.ListaId} no existe.");
                return ValidationProblem();
            }

            contexto.Tareas.Update(tarea);
            await contexto.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            int registrosBorrados = await contexto.Tareas.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                logger.LogWarning("No se encontró la tarea de id {Id} al intentar borrarla.", id);
                return NotFound();
            }

            return Ok();
        }
    }
}
