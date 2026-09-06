using Microsoft.AspNetCore.Mvc;

namespace TareasAPI.Controllers
{
    [ApiController]
    [Route("api/tiempos-de-vida")]
    public class TiemposDeVidaController(ServicioTransient transient1, ServicioTransient transient2, ServicioScoped scoped1, ServicioScoped scoped2, ServicioSingleton singleton) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Transient = new
                {
                    servicioTransient1 = transient1.ObtenerGuid,
                    servicioTransient2 = transient2.ObtenerGuid
                },

                Scoped = new
                {
                    servicioScoped1 = scoped1.ObtenerGuid,
                    servicioScoped2 = scoped2.ObtenerGuid
                },

                Singleton = singleton.ObtenerGuid
            });
        }
    }
}
