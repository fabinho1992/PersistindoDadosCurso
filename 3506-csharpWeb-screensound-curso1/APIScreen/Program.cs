using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContextBase>();
builder.Services.AddTransient<Dal<Artista>>();
builder.Services.AddTransient<Dal<Musica>>();

// essa configuração garante que as referências cíclicas sejam tratadas adequadamente durante a serialização JSON em um aplicativo ASP.NET Core.
//FAZENDO COM QUE CONSIGA MOSTRAR NO NAVEGADOR MEU JSON 
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
    options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();


// Configure the HTTP request pipeline.

//Artistas
app.MapGet("/Artistas", ([FromServices] Dal<Artista> dal) =>
{
    return dal.Listar();
});

app.MapGet("/Artistas/{nome}", ([FromServices] Dal<Artista> dal, string nome) =>
{
    
    var artista = dal.RecuperarPor(x => x.Nome.ToUpper().Equals(nome.ToUpper()));
    if (artista is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(artista);
});

app.MapPost("/Artistas", ([FromServices] Dal<Artista> dal, [FromBody] Artista artista) =>
{
    
    dal.Adicionar(artista);
    return Results.Ok();

});

app.MapDelete("/Artistas/{id}", ([FromServices] Dal<Artista> dal, int id) =>
{
    var artista = dal.RecuperarPor(x => x.Id == id);
    if (artista is null)
    {
        return Results.NotFound();
    }
    dal.Deletar(artista);
    return Results.NoContent();

});

app.MapPut("/Artistas", ([FromServices] Dal<Artista> dal, Artista artista) =>
{
    var artistaAtualizado = dal.RecuperarPor(x => x.Id == artista.Id);
    if (artistaAtualizado is null)
    {
        return Results.NotFound();
    }
    artistaAtualizado.Nome = artista.Nome;
    artistaAtualizado.Bio = artista.Bio;
    artistaAtualizado.FotoPerfil = artista.FotoPerfil;

    dal.Atualizar(artistaAtualizado);

    return Results.Ok();

});

// Musicas

app.MapGet("/Musicas", ([FromServices] Dal<Musica> dal) =>
{
    return dal.Listar();
});

app.MapGet("/Musicas/{nome}", ([FromServices] Dal<Musica> dal, string nome) =>
{

    var musica = dal.RecuperarPor(x => x.Nome.ToUpper().Equals(nome.ToUpper()));
    if (musica is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(musica);
});

app.MapPost("/Musicas", ([FromServices] Dal<Musica> dal, [FromBody] Musica musica) =>
{

    dal.Adicionar(musica);
    return Results.Ok();

});

app.MapDelete("/Musicas/{id}", ([FromServices] Dal<Musica> dal, int id) =>
{
    var musica = dal.RecuperarPor(x => x.Id == id);
    if (musica is null)
    {
        return Results.NotFound();
    }
    dal.Deletar(musica);
    return Results.NoContent();

});

app.MapPut("/Musicas", ([FromServices] Dal<Musica> dal, Musica musica) =>
{
    var musicaAtualizada = dal.RecuperarPor(x => x.Id == musica.Id);
    if (musicaAtualizada is null)
    {
        return Results.NotFound();
    }
    musicaAtualizada.Nome = musica.Nome;
    musicaAtualizada.AnoLancamento = musica.AnoLancamento;
    

    dal.Atualizar(musicaAtualizada);

    return Results.Ok();

});


app.Run();


