using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace APIScreen.EndPoints
{
    public static class ArtistasExtensions
    {
        public static void AddEndPointsArtistas( this WebApplication app)
        {
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

            app.MapPut("/Artistas", ([FromServices] Dal<Artista> dal, [FromBody] Artista artista) =>
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
        }
    }
}
