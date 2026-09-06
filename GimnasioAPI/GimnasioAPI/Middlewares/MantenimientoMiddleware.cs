namespace GimnasioAPI.Middlewares
{
    public class MantenimientoMiddleware(RequestDelegate siguiente, IConfiguration configuracion, ILogger<MantenimientoMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext contexto)
        {
            if (configuracion.GetValue<bool>("Mantenimiento") && contexto.Request.Path.StartsWithSegments("/api"))
            {
                logger.LogWarning("Petición a ruta {Ruta} bloqueada: modo mantenimiento activo.", contexto.Request.Path);

                contexto.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await contexto.Response.WriteAsync("API en mantenimiento.");

                return;
            }

            await siguiente.Invoke(contexto);
        }
    }

    public static class MantenimientoMiddlewareExtensions
    {
        public static IApplicationBuilder UseMantenimientoMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<MantenimientoMiddleware>();
    }
}
