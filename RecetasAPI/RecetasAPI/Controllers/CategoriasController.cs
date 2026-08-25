using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetasAPI.Datos;
using RecetasAPI.Entidades;

namespace RecetasAPI.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController(ApplicationDbContext contexto, ILogger<CategoriasController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            logger.LogInformation("Obteniendo el listado de categorías.");
            return await contexto.Categorias.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Categoria>> GetCategoriaPorId([FromRoute] int id, [FromQuery] bool? incluirRecetas)
        {
            IQueryable<Categoria> consulta = contexto.Categorias;

            if (incluirRecetas is true)
                consulta = consulta.Include(x => x.Receta);

            Categoria? categoria = await consulta.FirstOrDefaultAsync(x => x.Id == id);

            if (categoria is null)
            {
                logger.LogWarning("No se encontró la categoría de id {Id} al intentar buscarla.", id);
                return NotFound();
            }

            return categoria;
        }

        [HttpGet("{texto:regex(^[[a-zA-Z ]]+$)}")]
        public async Task<IEnumerable<Categoria>> GetCategoriasPorTexto([FromRoute] string texto, [FromQuery] bool? incluirRecetas)
        {
            IQueryable<Categoria> consulta = contexto.Categorias;

            if (incluirRecetas is true)
                consulta = consulta.Include(x => x.Receta);

            return await consulta.Where(x => x.Nombre.Contains(texto)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> PostCategoria([FromBody] Categoria categoria)
        {
            try
            {
                contexto.Add(categoria);
                await contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al guardar la categoría {Nombre}.", categoria.Nombre);
                return StatusCode(500, "No se pudo guardar la categoría.");
            }

            return CreatedAtAction(nameof(GetCategoriaPorId), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutCategoria([FromRoute] int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                ModelState.AddModelError(nameof(categoria.Id), "Las ids deben coincidir.");
                return ValidationProblem();
            }

            bool existeCategoria = await contexto.Categorias.AnyAsync(x => x.Id == id);

            if (!existeCategoria)
            {
                logger.LogWarning("No se encontró la categoría de id {Id} al intentar actualizarla.", id);
                return NotFound();
            }

            contexto.Update(categoria);
            await contexto.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategoria([FromRoute] int id)
        {
            int registrosBorrados = await contexto.Categorias.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                logger.LogWarning("No se encontró la categoría de id {Id} al intentar eliminarla.", id);
                return NotFound();
            }

            return Ok();
        }
    }
}
