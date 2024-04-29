using APIScreen.Request.Artista;
using APIScreen.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace APIScreen.EndPoints
{
    public static class ArtistasExtensions
    {

        private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
        {
            return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
        }

        private static ArtistaResponse EntityToResponse(Artista artista)
        {
            return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
        }


        public static void AddEndPointsArtistas(this WebApplication app)
        {
            app.MapGet("/Artistas", ([FromServices] Dal<Artista> dal) =>
            {
                var lista = dal.Listar();
                if (lista is null)
                {
                    return Results.NotFound();
                }
                var listaResponse = EntityListToResponseList(lista);
                return Results.Ok(listaResponse);
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] Dal<Artista> dal, string nome) =>
            {

                var artista = dal.RecuperarPor(x => x.Nome.ToUpper().Equals(nome.ToUpper()));
                if (artista is null)
                {
                    return Results.NotFound();
                }
                var artistaResponse = EntityToResponse(artista);
                return Results.Ok(artistaResponse);
            });

            app.MapPost("/Artistas", ([FromServices] Dal<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
            {
                var artista = new Artista { Nome = artistaRequest.Nome, Bio = artistaRequest.Bio }; 
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

            app.MapPut("/Artistas", ([FromServices] Dal<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequest) =>
            {
                var artistaAtualizado = dal.RecuperarPor(x => x.Id == artistaRequest.Id);
                if (artistaAtualizado is null)
                {
                    return Results.NotFound();
                }
                artistaAtualizado.Nome = artistaRequest.Nome;
                artistaAtualizado.Bio = artistaRequest.Bio;
                artistaAtualizado.FotoPerfil = artistaRequest.FotoPerfil;

                dal.Atualizar(artistaAtualizado);

                return Results.Ok();

            });
        }
    }
}
