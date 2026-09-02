using GimnasioAPI2.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Área de Servicios

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// Área de Middlewares

app.MapControllers();
app.Run();
