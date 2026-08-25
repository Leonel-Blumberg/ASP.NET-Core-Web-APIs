using Microsoft.AspNetCore.Mvc;

namespace TareasAPI.Controllers
{
    [ApiController]
    [Route("api/tiempos-de-vida")]
    public class TiemposDeVidaController(ServicioTransient servicioTransient1, ServicioTransient servicioTransient2, ServicioScoped servicioScoped1, ServicioScoped servicioScoped2, ServicioSingleton servicioSingleton) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Transient = new
                {
                    servicioTransient1 = servicioTransient1.ObtenerGuid,
                    servicioTransient2 = servicioTransient2.ObtenerGuid
                },
                Scoped = new
                {
                    servicioScoped1 = servicioScoped1.ObtenerGuid,
                    servicioScoped2 = servicioScoped2.ObtenerGuid
                },
                Singleton = servicioSingleton.ObtenerGuid
            });
        }
    }
}
