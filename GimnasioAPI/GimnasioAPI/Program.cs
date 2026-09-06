using GimnasioAPI;
using GimnasioAPI.Datos;
using GimnasioAPI.Interfaces;
using GimnasioAPI.Middlewares;
using GimnasioAPI.Repositorios;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Área de Servicios

builder.Services.AddControllers().AddNewtonsoftJson(opciones => opciones.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddAutoMapper(configuracion => configuracion.AddMaps(typeof(Program).Assembly));
builder.Services.AddSingleton<IRepositorioResenas, RepositorioResenasLista>();

builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// Área de Middlewares

app.UseTiempoDeRespuestaMiddleware();
app.UseMantenimientoMiddleware();

app.MapControllers();

app.Run();
