using AutoMapper;
using GimnasioAPI.Datos;
using GimnasioAPI.DTOs.Resena;
using GimnasioAPI.Entidades;
using GimnasioAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GimnasioAPI.Controllers
{
    [ApiController]
    [Route("api/clases/{claseId:int}/resenas")]
    public class ResenasController(ApplicationDbContext contexto, IRepositorioResenas resenas, IMapper mapper, ILogger<ResenasController> logger) : ControllerBase
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

            IReadOnlyCollection<Resena> resenasDeLaClase = resenas.ObtenerPorClase(claseId);
            List<ResenaDTO> resenaDTOs = mapper.Map<List<ResenaDTO>>(resenasDeLaClase);
            return resenaDTOs;
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

            Resena? resena = resenas.ObtenerPorClase(claseId).FirstOrDefault(x => x.Id == guid);

            if (resena is null)
            {
                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("No se encontro la guid {guid} al intentar buscarla.", guid);

                return NotFound();
            }

            ResenaDTO resenaDTO = mapper.Map<ResenaDTO>(resena);
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

            Resena resena = resenas.Crear(claseId, resenaCreacionDTO);

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Se creó la reseña {guid} para la clase {claseId}.", resena.Id, claseId);

            ResenaDTO resenaDTO = mapper.Map<ResenaDTO>(resena);
            return CreatedAtRoute("ObtenerResena", new { claseId, guid = resenaDTO.Id }, resenaDTO);
        }
    }
}
