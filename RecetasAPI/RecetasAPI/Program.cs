using Microsoft.EntityFrameworkCore;
using RecetasAPI;
using RecetasAPI.Datos;
using RecetasAPI.Interfaces;
using RecetasAPI.Middlewares;
using RecetasAPI.Repositorios;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Área de Servicios

builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();

builder.Services.AddSingleton<IRepositorioComentarios, RepositorioComentariosEnMemoria>();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer("Name=DefaultConnection"));

var app = builder.Build();

// Área de Middlewares

app.UseTiempoDeRespuesta();
app.UseMantenimientoMiddleware();

app.MapControllers();
app.Run();
