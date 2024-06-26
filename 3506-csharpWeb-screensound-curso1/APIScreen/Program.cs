using APIScreen.EndPoints;
using APIScreen.Request.Artista;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Modelos.Modelos;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContextBase>();
builder.Services.AddTransient<Dal<Artista>>();
builder.Services.AddTransient<Dal<Musica>>();
builder.Services.AddTransient<Dal<Genero>>();

// essa configura��o garante que as refer�ncias c�clicas sejam tratadas adequadamente durante a serializa��o JSON em um aplicativo ASP.NET Core.
//FAZENDO COM QUE CONSIGA MOSTRAR NO NAVEGADOR MEU JSON 
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
    options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScreenSound", Version = "v1" });
});

//AutoMapper
var config = new AutoMapper.MapperConfiguration(cfg =>
 {
     cfg.CreateMap<ArtistaRequest, Artista>();
 });

IMapper mapper = config.CreateMapper();
var app = builder.Build();

app.AddEndPointsArtistas();
app.AddEndPointsMusicas();
app.AddEndPointsGeneros();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();


