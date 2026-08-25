using Microsoft.AspNetCore.Mvc;

namespace RecetasAPI.Controllers
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
                    transient1 = transient1.ObtenerGuid,
                    transient2 = transient2.ObtenerGuid
                },
                Scoped = new
                {
                    scoped1 = scoped1.ObtenerGuid,
                    scoped2 = scoped2.ObtenerGuid
                },
                singleton = singleton.ObtenerGuid
            });
        }
    }
}
