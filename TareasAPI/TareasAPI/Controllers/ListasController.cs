using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasAPI.Datos;
using TareasAPI.Entidades;

namespace TareasAPI.Controllers
{
    [ApiController]
    [Route("api/listas")]
    public class ListasController(ApplicationDbContext contexto, ILogger<ListasController> logger) : ControllerBase
    {

        [HttpGet]
        public async Task<IEnumerable<Lista>> Get()
        {
            logger.LogInformation("Obteniendo el listado de listas.");
            return await contexto.Listas.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "ObtenerListaPorId")]
        public async Task<ActionResult<Lista>> Get([FromRoute] int id)
        {
            Lista? lista = await contexto.Listas.Include(x => x.Tareas).FirstOrDefaultAsync(x => x.Id == id);

            if (lista is null)
            {
                logger.LogWarning("No se encontró la lista de id {Id} al intentar buscarla.", id);
                return NotFound();
            }

            return lista;
        }

        [HttpGet("{nombre:alpha}")]
        public async Task<IEnumerable<Lista>> Get([FromRoute] string nombre)
        {
            return await contexto.Listas.Include(x => x.Tareas).Where(x => x.Nombre.Contains(nombre)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Lista lista)
        {
            try
            {
                contexto.Listas.Add(lista);
                await contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar la lista {Nombre}.", lista.Nombre);
                return StatusCode(500, "No se pudo guardar la lista.");
            }

            return CreatedAtRoute("ObtenerListaPorId", new { id = lista.Id }, lista);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] Lista lista)
        {
            if (id != lista.Id)
            {
                ModelState.AddModelError(nameof(lista.Id), "Las ids deben coincidir.");
                return ValidationProblem();
            }

            bool existeLista = await contexto.Listas.AnyAsync(x => x.Id == id);

            if (!existeLista)
            {
                logger.LogWarning("No se encontró la lista de id {Id} al intentar actualizarla.", id);
                return NotFound();
            }

            contexto.Listas.Update(lista);
            await contexto.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            int registrosBorrados = await contexto.Listas.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                logger.LogWarning("No se encontró la lista de id {Id} al intentar borrarla.", id);
                return NotFound();
            }

            return Ok();
        }
    }
}
