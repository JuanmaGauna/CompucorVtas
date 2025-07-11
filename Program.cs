using CompucorVtas.Data;
using CompucorVtas.Middlewares;
using CompucorVtas.Validators;
using CompucorVtas.Services;
using CompucorVtas.Mappings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IVentaService, VentaService>();
// Servicios de aplicación
builder.Services.AddScoped<IProductoService, ProductoService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ProductoValidator>();

// Entity Framework + SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compucorvtas.db"));

// Swagger/OpenAPI
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

    // Comentarios XML
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Controllers
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

// Middleware global de errores
app.UseMiddleware<ExceptionMiddleware>();
app.UseErrorHandler();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Compucor Ventas API v1");
    c.RoutePrefix = "swagger";
});

// Middleware estándar
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
