using GimnasioAPI.Datos;
using GimnasioAPI.DTOs.Resena;
using GimnasioAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GimnasioAPI.Controllers
{
    [ApiController]
    [Route("api/clases/{claseId:int}/resenas")]
    public class ResenasController(ApplicationDbContext contexto, IRepositorioResenas resenas, ILogger<ResenasController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResenaDTO>>> Get([FromRoute] int claseId)
        {
            bool existeClaseId = await contexto.Clases.AnyAsync(x => x.Id == claseId);

            if (!existeClaseId)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("No se encontro la id {claseId} al intentar buscarla.", claseId);

                return NotFound();
            }

            IEnumerable<ResenaDTO> resenaDTOs = resenas.ObtenerPorClase(claseId);
            return resenaDTOs.ToList();
        }

        [HttpGet("{guid:Guid}", Name = "ObtenerResena")]
        public async Task<ActionResult<ResenaDTO>> GetPorId([FromRoute] int claseId, [FromRoute] Guid guid)
        {
            bool existeClaseId = await contexto.Clases.AnyAsync(x => x.Id == claseId);

            if (!existeClaseId)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("No se encontro la id {claseId} al intentar buscarla.", claseId);

                return NotFound();
            }

            ResenaDTO? resenaDTO = resenas.ObtenerPorClase(claseId).FirstOrDefault(x => x.Id == guid);

            if (resenaDTO is null)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("No se encontro la guid {guid} al intentar buscarla.", guid);

                return NotFound();
            }

            return resenaDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromRoute] int claseId, [FromBody] ResenaCreacionDTO resenaCreacionDTO)
        {
            bool existeClaseId = await contexto.Clases.AnyAsync(x => x.Id == claseId);

            if (!existeClaseId)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("No se encontro la id {claseId} al intentar buscarla.", claseId);

                return NotFound();
            }

            ResenaDTO resenaDTO = resenas.Crear(claseId, resenaCreacionDTO);

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se creó la reseña {guid} para la clase {claseId}.", resenaDTO.Id,claseId);

            return CreatedAtRoute("ObtenerResena", new { claseId, guid = resenaDTO.Id }, resenaDTO);
        }
    }
}
