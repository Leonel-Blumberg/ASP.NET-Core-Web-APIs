using System.Data.Common;
using AutoMapper;
using GimnasioAPI.Datos;
using GimnasioAPI.DTOs.Disciplina;
using GimnasioAPI.Entidades;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GimnasioAPI.Controllers
{
    [ApiController]
    [Route("api/disciplinas")]
    public class DisciplinasController(ApplicationDbContext contexto, IMapper mapper, ILogger<DisciplinasController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<DisciplinaDTO>> Get()
        {
            List<Disciplina> disciplinas = await contexto.Disciplinas.ToListAsync();
            IEnumerable<DisciplinaDTO> disciplinasDTO = mapper.Map<IEnumerable<DisciplinaDTO>>(disciplinas);
            return disciplinasDTO;
        }

        [HttpGet("{id:int}", Name = "ObtenerDisciplina")]
        public async Task<ActionResult<DisciplinaConClasesDTO>> GetPorId([FromRoute] int id)
        {
            Disciplina? disciplina = await contexto.Disciplinas.Include(x => x.Clases).FirstOrDefaultAsync(x => x.Id == id);

            if (disciplina is null)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("No se encontro la id {id} al intentar buscarla.", id);

                return NotFound();
            }

            DisciplinaConClasesDTO disciplinaDTO = mapper.Map<DisciplinaConClasesDTO>(disciplina);
            return disciplinaDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaCreacionDTO disciplinaCreacionDTO)
        {
            Disciplina disciplina = mapper.Map<Disciplina>(disciplinaCreacionDTO);
            contexto.Add(disciplina);

            await contexto.SaveChangesAsync();

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se creo la disciplina {id}.", disciplina.Id);

            DisciplinaDTO disciplinaDTO = mapper.Map<DisciplinaDTO>(disciplina);
            return CreatedAtRoute("ObtenerDisciplina", new { id = disciplina.Id }, disciplinaDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] DisciplinaCreacionDTO disciplinaCreacionDTO)
        {
            bool existeId = await contexto.Disciplinas.AnyAsync(x => x.Id == id);

            if (!existeId)
            {
                logger.LogWarning("No se encontro la id {id} al intentar actualizarla.", id);
                return NotFound();
            }

            Disciplina disciplina = mapper.Map<Disciplina>(disciplinaCreacionDTO);
            disciplina.Id = id;

            contexto.Update(disciplina);
            await contexto.SaveChangesAsync();

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se actualizo la disciplina {id}.", id);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<DisciplinaPatchDTO> patchDoc)
        {
            if (patchDoc is null)
            {
                logger.LogWarning("La disciplina ingresada al hacer la petición patch es nula.");
                return BadRequest();
            }

            Disciplina? disciplina = await contexto.Disciplinas.FirstOrDefaultAsync(x => x.Id == id);

            if (disciplina is null)
            {
                logger.LogWarning("No se encontro la id {id} al intentar hacer la petición patch.", id);
                return NotFound();
            }

            DisciplinaPatchDTO disciplinaPatchDTO = mapper.Map<DisciplinaPatchDTO>(disciplina);

            patchDoc.ApplyTo(disciplinaPatchDTO, ModelState);

            if (!TryValidateModel(disciplinaPatchDTO))
            {
                logger.LogWarning("La disciplina ingresada al hacer la petición patch es erronea.");
                return ValidationProblem();
            }

            mapper.Map(disciplinaPatchDTO, disciplina);

            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                int registrosBorrados = await contexto.Disciplinas.Where(x => x.Id == id).ExecuteDeleteAsync();

                if (registrosBorrados == 0)
                {
                    logger.LogWarning("No se encontro la id {id} al intentar borrarla.", id);
                    return NotFound();
                }

                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("Se borro la disciplina {id}.", id);

                return NoContent();
            }
            catch (DbException ex)
            {
                logger.LogError(ex, "Error al borrar la disciplina {id}.", id);
                ModelState.AddModelError(nameof(id), "No se puede borrar una disciplina que tiene clases asociadas.");
                return ValidationProblem();
            }
        }
    }
}
