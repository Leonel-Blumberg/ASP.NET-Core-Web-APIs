namespace TareasAPI.Middlewares
{
    public class MantenimientoMiddleware(RequestDelegate next, IConfiguration configuracion)
    {
        public async Task InvokeAsync(HttpContext contexto)
        {
            if (configuracion.GetValue<bool>("Mantenimiento") && contexto.Request.Path.StartsWithSegments("/api"))
            {
                contexto.Response.StatusCode = 503;
                await contexto.Response.WriteAsync("API en mantenimiento");

                ILogger<Program> logger = contexto.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogWarning("Petición a {Ruta} bloqueada: modo mantenimiento activo.", contexto.Request.Path);

                return;
            }

            await next.Invoke(contexto);
        }
    }

    public static class MantenimientoMiddlewareExtensions
    {
        public static IApplicationBuilder UseMantenimiento(this IApplicationBuilder builder)
            => builder.UseMiddleware<MantenimientoMiddleware>();
    }
}
