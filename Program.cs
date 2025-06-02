using CompucorVtas.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using CompucorVtas.Validators;
using FluentValidation;
using CompucorVtas.Middlewares;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework + SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compucorvtas.db"));

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Compucor Ventas API",
        Version = "v1",
        Description = "API REST para la gestión de productos, clientes y categorías",
        Contact = new OpenApiContact
        {
            Name = "Juan Manuel Gauna",
            Url = new Uri("https://github.com/juanmagauna/CompucorVtas")
        }
    });

    c.EnableAnnotations();
});

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<ProductoValidator>();

var app = builder.Build();

app.UseMiddleware<CompucorVtas.Middlewares.ExceptionMiddleware>();

// Middleware de manejo de errores global
app.UseErrorHandler();


    app.UseSwagger();

    // 🔁 Cambiamos la ruta para evitar el cache roto de Swagger UI
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Compucor Ventas API v1");
        c.RoutePrefix = "swagger"; // Swagger se abrirá en /swagger
    });


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
