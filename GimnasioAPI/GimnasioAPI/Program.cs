using GimnasioAPI.Datos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Área de Servicios

builder.Services.AddControllers().AddNewtonsoftJson(opciones => opciones.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddAutoMapper(configuracion => configuracion.AddMaps(typeof(Program).Assembly));

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// Área de Middlewares

app.MapControllers();
app.Run();
