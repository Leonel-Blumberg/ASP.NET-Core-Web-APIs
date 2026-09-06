using System.Diagnostics;

namespace TareasAPI.Middlewares
{
    public class TiempoDeRespuestaMiddleware(RequestDelegate next, ILogger<TiempoDeRespuestaMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext contexto)
        {
            Stopwatch reloj = Stopwatch.StartNew();

            try
            {
                await next.Invoke(contexto);
            }
            finally
            {
                reloj.Stop();

                if (logger.IsEnabled(LogLevel.Information))
                    logger.LogInformation("Petición {Metodo} {Ruta} respondió {StatusCode} en {Ms}ms.", contexto.Request.Method, contexto.Request.Path, contexto.Response.StatusCode, reloj.ElapsedMilliseconds);
            }
        }
    }

    public static class TiempoDeRespuestaMiddlewareExtensions
    {
        public static IApplicationBuilder UseTiempoDeRespuesta(this IApplicationBuilder builder)
            => builder.UseMiddleware<TiempoDeRespuestaMiddleware>();
    }
}
