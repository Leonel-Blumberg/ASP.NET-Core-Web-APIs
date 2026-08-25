using System.Diagnostics;

namespace RecetasAPI.Middlewares
{
    public class TiempoDeRespuestaMiddleware(RequestDelegate next, ILogger<TiempoDeRespuestaMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext contexto)
        {
            Stopwatch reloj = Stopwatch.StartNew();

            await next.Invoke(contexto);

            reloj.Stop();

            logger.LogInformation("Petición {Metodo} {Ruta} respondió {StatusCode} en {Ms}ms.", contexto.Request.Method, contexto.Request.Path, contexto.Response.StatusCode, reloj.ElapsedMilliseconds);
        }
    }

    public static class TiempoDeRespuestaMiddlewareExtensions
    {
        public static IApplicationBuilder UseTiempoDeRespuesta(this IApplicationBuilder builder) => builder.UseMiddleware<TiempoDeRespuestaMiddleware>();
    }
}
