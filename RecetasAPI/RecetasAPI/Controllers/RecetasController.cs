using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetasAPI.Datos;
using RecetasAPI.Entidades;

namespace RecetasAPI.Controllers
{
    [ApiController]
    [Route("api/recetas")]
    public class RecetasController(ApplicationDbContext contexto, ILogger<RecetasController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Receta>> GetRecetas()
        {
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Obteniendo el listado de recetas.");

            return await contexto.Recetas.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Receta>> GetRecetaPorId([FromRoute] int id, [FromQuery] bool? incluirCategoria)
        {
            IQueryable<Receta> consulta = contexto.Recetas;

            if (incluirCategoria is true)
                consulta = consulta.Include(x => x.Categoria);

            Receta? receta = await consulta.FirstOrDefaultAsync(x => x.Id == id);

            if (receta is null)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("No se encontró la receta de id {id} al intentar buscarla.", id);

                return NotFound();
            }

            return receta;
        }

        [HttpGet("{texto:regex(^[[a-zA-Z ]]+$)}")]
        public async Task<IEnumerable<Receta>> GetRecetasPorTexto([FromRoute] string texto, [FromHeader(Name = "solo-vegetarianas")] bool? soloVegetarianas, [FromQuery] bool? incluirCategoria)
        {
            IQueryable<Receta> consulta = contexto.Recetas;

            if (incluirCategoria is true)
                consulta = consulta.Include(x => x.Categoria);

            if (soloVegetarianas is true)
                consulta = consulta.Where(x => x.Vegetariana);

            return await consulta.Where(x => x.Titulo.Contains(texto)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> PostReceta([FromBody] Receta receta)
        {
            bool existeCategoria = await contexto.Categorias.AnyAsync(x => x.Id == receta.CategoriaId);

            if (!existeCategoria)
            {
                ModelState.AddModelError(nameof(receta.CategoriaId), $"La categoría de id {receta.CategoriaId} no existe.");
                return ValidationProblem();
            }

            try
            {
                contexto.Add(receta);
                await contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar la receta {titulo}.", receta.Titulo);
                return StatusCode(500, "No se pudo guardar la receta.");
            }

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se creó la receta {id}.", receta.Id);

            return CreatedAtAction(nameof(GetRecetaPorId), new { id = receta.Id }, receta);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutReceta([FromRoute] int id, [FromBody] Receta receta)
        {
            if (id != receta.Id)
            {
                ModelState.AddModelError(nameof(receta.Id), "Las ids deben coincidir.");
                return ValidationProblem();
            }

            bool existeReceta = await contexto.Recetas.AnyAsync(x => x.Id == id);

            if (!existeReceta)
            {
                logger.LogWarning("No se encontró la receta de id {id} al intentar actualizarla.", id);
                return NotFound();
            }

            bool existeCategoria = await contexto.Categorias.AnyAsync(x => x.Id == receta.CategoriaId);

            if (!existeCategoria)
            {
                ModelState.AddModelError(nameof(receta.CategoriaId), $"La id {receta.CategoriaId} no existe.");
                return ValidationProblem();
            }

            contexto.Update(receta);
            await contexto.SaveChangesAsync();

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se actualizó la receta {id}.", id);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteReceta([FromRoute] int id)
        {
            int registrosBorrados = await contexto.Recetas.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                logger.LogWarning("No se encontró la receta de id {id} al intentar eliminarla.", id);
                return NotFound();
            }

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se borró la receta {id}.", id);

            return Ok();
        }
    }
}
