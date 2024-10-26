using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tarea API",
        Version = "v1",
        Description = "Documentacion de mi API con Swagger"
    });
});

var app = builder.Build();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
        c.RoutePrefix = string.Empty;
    });
};



app.MapGet("/noticias", () => parte_1.Ejecutar());

app.MapPost("/Registro_Agentes", (Agentes a) => Manejador_Agente.Registro(a));

app.MapPost("/iniciar_sesion", (DatosLogin dl) => Manejador_Agente.IniciarSesion(dl));

app.MapPost("/RegistrarIncidencia", (Incidencia i) => Manejador_Agente.Registro_Incidencia(i));

app.MapGet("/clima", async (string latitud, string longitud) => 
{
    var climaServicio = new ClimaServicio();
    return await climaServicio.ObtenerClimaAsync(latitud, longitud);
});

app.Run();
