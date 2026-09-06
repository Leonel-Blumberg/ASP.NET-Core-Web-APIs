using System.Diagnostics;

namespace GimnasioAPI.Middlewares
{
    public class TiempoDeRespuestaMiddleware(RequestDelegate siguiente, ILogger<TiempoDeRespuestaMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext contexto)
        {
            Stopwatch reloj = Stopwatch.StartNew();

            try
            {
                await siguiente.Invoke(contexto);
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
        public static IApplicationBuilder UseTiempoDeRespuestaMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<TiempoDeRespuestaMiddleware>();
    }
}
