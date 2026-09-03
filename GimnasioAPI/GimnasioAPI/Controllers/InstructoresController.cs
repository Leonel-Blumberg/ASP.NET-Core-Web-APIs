using AutoMapper;
using GimnasioAPI.Datos;
using GimnasioAPI.DTOs.Instructor;
using GimnasioAPI.Entidades;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GimnasioAPI.Controllers
{
    [ApiController]
    [Route("api/instructores")]
    public class InstructoresController(ApplicationDbContext contexto, IMapper mapper, ILogger<InstructoresController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<InstructorDTO>> Get()
        {
            List<Instructor> instructores = await contexto.Instructores.ToListAsync();
            IEnumerable<InstructorDTO> instructoresDTO = mapper.Map<IEnumerable<InstructorDTO>>(instructores);
            return instructoresDTO;
        }

        [HttpGet("{id:int}", Name = "ObtenerInstructor")]
        public async Task<ActionResult<InstructorConClasesDTO>> GetPorId([FromRoute] int id)
        {
            Instructor? instructor = await contexto.Instructores.Include(x => x.Clases).ThenInclude(x => x.Clase).FirstOrDefaultAsync(x => x.Id == id);

            if (instructor is null)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("No se encontro la id {id} al intentar buscarla.", id);

                return NotFound();
            }

            InstructorConClasesDTO instructorConClasesDTO = mapper.Map<InstructorConClasesDTO>(instructor);
            return instructorConClasesDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InstructorCreacionDTO instructorCreacionDTO)
        {
            Instructor instructor = mapper.Map<Instructor>(instructorCreacionDTO);
            contexto.Add(instructor);

            await contexto.SaveChangesAsync();

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se creo el instructor {id}.", instructor.Id);

            InstructorDTO instructorDTO = mapper.Map<InstructorDTO>(instructor);
            return CreatedAtRoute("ObtenerInstructor", new { id = instructor.Id }, instructorDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] InstructorCreacionDTO instructorCreacionDTO)
        {
            bool existeId = await contexto.Instructores.AnyAsync(x => x.Id == id);

            if (!existeId)
            {
                logger.LogWarning("No se encontro la id {id} al intentar actualizarla.", id);
                return NotFound();
            }

            Instructor instructor = mapper.Map<Instructor>(instructorCreacionDTO);
            instructor.Id = id;

            contexto.Update(instructor);
            await contexto.SaveChangesAsync();

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se actualizo el instructor {id}.", id);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<InstructorPatchDTO> patchDoc)
        {
            if (patchDoc is null)
            {
                logger.LogWarning("El instructor ingresado al hacer la petición patch es nulo.");
                return BadRequest();
            }

            Instructor? instructor = await contexto.Instructores.FirstOrDefaultAsync(x => x.Id == id);

            if (instructor is null)
            {
                logger.LogWarning("No se encontro la id {id} al intentar hacer la petición patch.", id);
                return NotFound();
            }

            InstructorPatchDTO instructorPatchDTO = mapper.Map<InstructorPatchDTO>(instructor);

            patchDoc.ApplyTo(instructorPatchDTO, ModelState);

            if (!TryValidateModel(instructorPatchDTO))
            {
                logger.LogWarning("El instructor ingresado al hacer la petición patch es erroneo.");
                return ValidationProblem();
            }

            mapper.Map(instructorPatchDTO, instructor);

            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            int registrosBorrados = await contexto.Instructores.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                logger.LogWarning("No se encontro la id {id} al intentar borrarla.", id);
                return NotFound();
            }

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se borro el instructor {id}.", id);

            return NoContent();
        }
    }
}
