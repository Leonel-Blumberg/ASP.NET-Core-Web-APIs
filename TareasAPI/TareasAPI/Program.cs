using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TareasAPI;
using TareasAPI.Datos;
using TareasAPI.Interfaces;
using TareasAPI.Middlewares;
using TareasAPI.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Área de servicios

builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();

builder.Services.AddSingleton<IRepositorioNotas, RepositorioNotasEnMemoria>();

builder.Services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// Área de Middlewares

app.UseTiempoDeRespuesta();
app.UseMantenimiento();

app.MapControllers();
app.Run();
