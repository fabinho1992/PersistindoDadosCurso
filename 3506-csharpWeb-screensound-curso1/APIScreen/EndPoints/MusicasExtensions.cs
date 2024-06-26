﻿using APIScreen.Request.Genero;
using APIScreen.Request.Musica;
using APIScreen.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Modelos.Modelos;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace APIScreen.EndPoints
{
    public static class MusicasExtensions
    {
        private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
        {
            return musicaList.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista.Nome);
        }

        private static ICollection<Genero> ConverterGeneroRequest(IEnumerable<GeneroRequest> generos, [FromServices] Dal<Genero> dal)
        {
            var listaGeneros = new List<Genero>();
            foreach (var item in generos)
            {
                var entity = RequestToEntity(item);
                var genero = dal.RecuperarPor(g => g.Nome.ToUpper().Equals(item.Nome.ToUpper()));
                if (genero is not null)
                {
                    listaGeneros.Add(genero);
                }
                else
                {
                    listaGeneros.Add(entity);
                }
            }
            return listaGeneros;
        }

        private static Genero RequestToEntity(GeneroRequest generoRequest)
        {
            return new Genero { Nome = generoRequest.Nome, Descricao = generoRequest.Descricao };
        }

        public static void AddEndPointsMusicas(this WebApplication app)
        {
            app.MapGet("/Musicas", ([FromServices] Dal<Musica> dal) =>
            {
                var lista = dal.Listar();
                if (lista is null)
                {
                    return Results.NotFound();
                }
                var listaResponse = EntityListToResponseList(lista);
                return Results.Ok(listaResponse);
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] Dal<Musica> dal, string nome) =>
            {

                var musica = dal.RecuperarPor(x => x.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica is null)
                {
                    return Results.NotFound();
                }
                var musicaResponse = EntityToResponse(musica);
                return Results.Ok(musicaResponse);
            });

            app.MapPost("/Musicas", ([FromServices] Dal<Musica> dal, [FromBody] MusicaRequest musicaRequest, [FromServices] Dal<Genero> dalGenero) =>
            {
                var musica = new Musica { ArtistaId = musicaRequest.ArtistaId, Nome = musicaRequest.Nome, AnoLancamento = musicaRequest.AnoLancamento, Generos = ConverterGeneroRequest(musicaRequest.Generos, dalGenero) };
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

            app.MapPut("/Musicas", ([FromServices] Dal<Musica> dal, MusicaRequestEdit musicaRequest) =>
            {
                var musicaAtualizada = dal.RecuperarPor(x => x.Id == musicaRequest.Id);
                if (musicaAtualizada is null)
                {
                    return Results.NotFound();
                }
                musicaAtualizada.Nome = musicaRequest.Nome;
                musicaAtualizada.AnoLancamento = musicaRequest.AnoLancamento;


                dal.Atualizar(musicaAtualizada);

                return Results.Ok();

            });

        }

        
    }
}
