using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace APIScreen.EndPoints
{
    public static class MusicasExtensions
    {
        public static void AddEndPointsMusicas( this WebApplication app)
        {
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
        }
    }
}
