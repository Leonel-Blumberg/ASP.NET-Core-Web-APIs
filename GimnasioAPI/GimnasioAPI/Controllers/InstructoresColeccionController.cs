using AutoMapper;
using GimnasioAPI.Datos;
using GimnasioAPI.DTOs.Instructor;
using GimnasioAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GimnasioAPI.Controllers
{
    [ApiController]
    [Route("api/instructores-coleccion")]
    public class InstructoresColeccionController(ApplicationDbContext contexto, IMapper mapper, ILogger<InstructoresColeccionController> logger) : ControllerBase
    {
        [HttpGet("{ids}", Name = "ObtenerInstructores")]
        public async Task<ActionResult<List<InstructorConClasesDTO>>> Get([FromRoute] string ids)
        {
            List<int> idsColeccion = [];

            foreach (string? id in ids.Split(","))
            {
                if (int.TryParse(id, out int idInt) && !idsColeccion.Contains(idInt))
                    idsColeccion.Add(idInt);
            }

            if (idsColeccion.Count == 0)
            {
                logger.LogWarning("No se recibio ninguna id valida en la coleccion {ids}.", ids);

                ModelState.AddModelError(nameof(ids), "Ningun Id fue encontrado.");
                return ValidationProblem();
            }

            List<Instructor> instructores = await contexto.Instructores.Include(x => x.Clases).ThenInclude(x => x.Clase).Where(x => idsColeccion.Contains(x.Id)).ToListAsync();

            if (instructores.Count != idsColeccion.Count)
            {
                logger.LogWarning("La cantidad de instructores y la cantidad de ids en la coleccion no coinciden");
                return NotFound();
            }

            List<InstructorConClasesDTO> instructoresDTO = mapper.Map<List<InstructorConClasesDTO>>(instructores);
            return instructoresDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] List<InstructorCreacionDTO> instructoresCreacionDTO)
        {
            if (instructoresCreacionDTO.Count == 0)
            {
                logger.LogWarning("Se intento crear una coleccion de instructores vacia.");

                ModelState.AddModelError(nameof(instructoresCreacionDTO), "Hay que enviar al menos un instructor.");
                return ValidationProblem();
            }

            List<Instructor> instructores = mapper.Map<List<Instructor>>(instructoresCreacionDTO);
            contexto.AddRange(instructores);
            await contexto.SaveChangesAsync();

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se crearon {cantidad} instructores.", instructores.Count);

            List<InstructorDTO> instructoresDTO = mapper.Map<List<InstructorDTO>>(instructores);
            string idsString = string.Join(",", instructores.Select(x => x.Id));

            return CreatedAtRoute("ObtenerInstructores", new { ids = idsString }, instructoresDTO);
        }
    }
}
