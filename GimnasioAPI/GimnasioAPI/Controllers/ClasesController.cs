using AutoMapper;
using GimnasioAPI.Datos;
using GimnasioAPI.DTOs.Clase;
using GimnasioAPI.Entidades;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GimnasioAPI.Controllers
{
    [ApiController]
    [Route("api/clases")]
    public class ClasesController(ApplicationDbContext contexto, IMapper mapper, ILogger<ClasesController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<ClaseDTO>> Get([FromQuery] int? disciplinaId, [FromQuery] bool? activa)
        {
            IQueryable<Clase> queryable = contexto.Clases;

            if (disciplinaId is not null)
                queryable = queryable.Where(x => x.DisciplinaId == disciplinaId.Value);

            if (activa is not null)
                queryable = queryable.Where(x => x.Activa == activa.Value);

            List<Clase> clases = await queryable.ToListAsync();
            IEnumerable<ClaseDTO> clasesDTO = mapper.Map<IEnumerable<ClaseDTO>>(clases);
            return clasesDTO;
        }

        [HttpGet("{id:int}", Name = "ObtenerClase")]
        public async Task<ActionResult<ClaseDTO>> GetPorId([FromRoute] int id, [FromHeader(Name = "incluir-disciplina")] bool incluirDisciplina)
        {
            IQueryable<Clase> queryable = contexto.Clases;

            if (incluirDisciplina)
                queryable = queryable.Include(x => x.Instructores).ThenInclude(x => x.Instructor).Include(x => x.Disciplina);

            Clase? clase = await queryable.FirstOrDefaultAsync(x => x.Id == id);

            if (clase is null)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("No se encontro la id {id} al intentar buscarla.", id);

                return NotFound();
            }

            if (incluirDisciplina)
                return mapper.Map<ClaseConHijosDTO>(clase);

            return mapper.Map<ClaseDTO>(clase);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<IEnumerable<ClaseDTO>>> GetPorNombre([FromRoute] string nombre, [FromHeader(Name = "incluir-disciplina")] bool incluirDisciplina)
        {
            IQueryable<Clase> queryable = contexto.Clases;

            if (incluirDisciplina)
                queryable = queryable.Include(x => x.Instructores).ThenInclude(x => x.Instructor).Include(x => x.Disciplina);

            List<Clase> clases = await queryable.Where(x => x.Nombre.Contains(nombre)).ToListAsync();

            if (clases.Count == 0)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("No se encontro el nombre {nombre} al intentar buscarlo.", nombre);

                return NotFound();
            }

            IEnumerable<ClaseDTO> clasesDTO;

            if (incluirDisciplina)
                clasesDTO = mapper.Map<IEnumerable<ClaseConHijosDTO>>(clases);
            else
                clasesDTO = mapper.Map<IEnumerable<ClaseDTO>>(clases);

            return Ok(clasesDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClaseCreacionDTO claseCreacionDTO)
        {
            bool existeDisciplina = await contexto.Disciplinas.AnyAsync(x => x.Id == claseCreacionDTO.DisciplinaId);

            if (!existeDisciplina)
            {
                logger.LogWarning("No se encontro la disciplina {id} al intentar crear o actualizar una clase.", claseCreacionDTO.DisciplinaId);

                ModelState.AddModelError(nameof(claseCreacionDTO.DisciplinaId), $"La disciplina {claseCreacionDTO.DisciplinaId} no existe.");
                return ValidationProblem();
            }

            List<int> idsEnviadas = [..claseCreacionDTO.Instructores.Select(x => x.InstructorId)];

            if (idsEnviadas.Count != idsEnviadas.Distinct().Count())
            {
                logger.LogWarning("Se intento asignar un instructor repetido a la misma clase.");

                ModelState.AddModelError(nameof(claseCreacionDTO.Instructores), "No se puede repetir un instructor en la misma clase.");
                return ValidationProblem();
            }
            List<int> idsExistentes = await contexto.Instructores.Where(x => idsEnviadas.Contains(x.Id)).Select(x => x.Id).ToListAsync();
            List<int> idsFaltantes = [.. idsEnviadas.Except(idsExistentes)];

            if (idsFaltantes.Count > 0)
            {
                string stringIdsFaltantes = string.Join(",", idsFaltantes);

                logger.LogWarning("No existen los instructores con las ids {stringIdsFaltantes}.", stringIdsFaltantes);

                ModelState.AddModelError(nameof(claseCreacionDTO.Instructores), $"Los instructores con las siguientes ids no existen: {stringIdsFaltantes}.");
                return ValidationProblem();
            }

            Clase clase = mapper.Map<Clase>(claseCreacionDTO);
            contexto.Add(clase);

            await contexto.SaveChangesAsync();

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se creo la clase {id}.", clase.Id);

            ClaseDTO claseDTO = mapper.Map<ClaseDTO>(clase);
            return CreatedAtRoute("ObtenerClase", new { id = clase.Id }, claseDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] ClaseCreacionDTO claseCreacionDTO)
        {
            Clase? clase = await contexto.Clases.Include(x => x.Instructores).FirstOrDefaultAsync(x => x.Id == id);

            if (clase is null)
            {
                logger.LogWarning("No se encontro la id {id} al intentar actualizarla.", id);
                return NotFound();
            }

            bool existeDisciplina = await contexto.Disciplinas.AnyAsync(x => x.Id == claseCreacionDTO.DisciplinaId);

            if (!existeDisciplina)
            {
                logger.LogWarning("No se encontro la disciplina {id} al intentar crear o actualizar una clase.", claseCreacionDTO.DisciplinaId);

                ModelState.AddModelError(nameof(claseCreacionDTO.DisciplinaId), $"La disciplina {claseCreacionDTO.DisciplinaId} no existe.");
                return ValidationProblem();
            }

            List<int> idsEnviadas = [.. claseCreacionDTO.Instructores.Select(x => x.InstructorId)];

            if (idsEnviadas.Count != idsEnviadas.Distinct().Count())
            {
                logger.LogWarning("Se intento asignar un instructor repetido a la misma clase.");

                ModelState.AddModelError(nameof(claseCreacionDTO.Instructores), "No se puede repetir un instructor en la misma clase.");
                return ValidationProblem();
            }
            List<int> idsExistentes = await contexto.Instructores.Where(x => idsEnviadas.Contains(x.Id)).Select(x => x.Id).ToListAsync();
            List<int> idsFaltantes = [.. idsEnviadas.Except(idsExistentes)];

            if (idsFaltantes.Count > 0)
            {
                string stringIdsFaltantes = string.Join(",", idsFaltantes);

                logger.LogWarning("No existen los instructores con las ids {stringIdsFaltantes}.", stringIdsFaltantes);

                ModelState.AddModelError(nameof(claseCreacionDTO.Instructores), $"Los instructores con las siguientes ids no existen: {stringIdsFaltantes}.");
                return ValidationProblem();
            }

            contexto.RemoveRange(clase.Instructores);

            mapper.Map(claseCreacionDTO, clase);

            await contexto.SaveChangesAsync();

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se actualizo la clase {id}.", id);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<ClasePatchDTO> patchDoc)
        {
            if (patchDoc is null)
            {
                logger.LogWarning("La clase ingresada al hacer la petición patch es nula.");
                return BadRequest();
            }

            Clase? clase = await contexto.Clases.FirstOrDefaultAsync(x => x.Id == id);

            if (clase is null)
            {
                logger.LogWarning("No se encontro la id {id} al intentar hacer la petición patch.", id);
                return NotFound();
            }

            ClasePatchDTO clasePatchDTO = mapper.Map<ClasePatchDTO>(clase);

            patchDoc.ApplyTo(clasePatchDTO, ModelState);

            if (!TryValidateModel(clasePatchDTO))
            {
                logger.LogWarning("La clase ingresada al hacer la petición patch es erronea.");
                return ValidationProblem();
            }

            bool existeDisciplina = await contexto.Disciplinas.AnyAsync(x => x.Id == clasePatchDTO.DisciplinaId);

            if (!existeDisciplina)
            {
                logger.LogWarning("No se encontro la disciplina {id} al intentar parchear una clase.", clasePatchDTO.DisciplinaId);

                ModelState.AddModelError(nameof(clasePatchDTO.DisciplinaId), $"La disciplina {clasePatchDTO.DisciplinaId} no existe.");
                return ValidationProblem();
            }

            mapper.Map(clasePatchDTO, clase);

            await contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            int registrosBorrados = await contexto.Clases.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                logger.LogWarning("No se encontro la id {id} al intentar borrarla.", id);
                return NotFound();
            }

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se borro la clase {id}.", id);

            return NoContent();
        }
    }
}
