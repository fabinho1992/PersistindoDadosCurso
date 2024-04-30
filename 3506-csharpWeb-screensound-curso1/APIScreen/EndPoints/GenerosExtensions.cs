using APIScreen.Request.Genero;
using APIScreen.Request.Musica;
using APIScreen.Response;
using Microsoft.AspNetCore.Mvc;
using Modelos.Modelos;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace APIScreen.EndPoints
{
    public static class GenerosExtensions
    {
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

        private static GeneroResponse ResponseToEntity(Genero genero)
        {
            return new GeneroResponse { Nome = genero.Nome, Descricao = genero.Descricao };
        }

        private static Genero RequestToEntity(GeneroRequest generoRequest)
        {
            return new Genero { Nome = generoRequest.Nome, Descricao = generoRequest.Descricao };
        }

        private static ICollection<GeneroResponse> ConverterGeneroResponse(IEnumerable<Genero> generos)
        {
            return generos.Select(a => ResponseToEntity(a)).ToList();
        }

        public static void AddEndPointsGeneros(this WebApplication app)
        {
            //Generos

            app.MapPost("/Generos", ([FromServices] Dal<Genero> dal, [FromBody] GeneroRequest generoRequest, [FromServices] Dal<Genero> dalGenero) =>
            {
                
                var consulta = dal.RecuperarPor(g => g.Nome.ToUpper().Equals(generoRequest.Nome.ToUpper()));
                if (consulta is not null)
                {
                    return Results.NotFound("Genero já cadastrado!");
                }
                else
                {
                    dal.Adicionar(RequestToEntity(generoRequest));
                    
                }
                
                return Results.Ok();

            });


            app.MapGet("/Generos", ([FromServices] Dal<Genero> dal) =>
            {
                var lista = dal.Listar();
                if (lista is null)
                {
                    return Results.NotFound();
                }
                var listaResponse = ConverterGeneroResponse(lista);
                return Results.Ok(listaResponse);
            });


            app.MapGet("/Generos/{nome}", ([FromServices] Dal<Genero> dal, string nome) =>
            {

                var genero = dal.RecuperarPor(x => x.Nome.ToUpper().Equals(nome.ToUpper()));
                if (genero is null)
                {
                    return Results.NotFound();
                }
                var generoResponse = ResponseToEntity(genero);
                return Results.Ok(generoResponse);
            });

            app.MapDelete("/Generos/{id}", ([FromServices] Dal<Genero> dal, int id) =>
            {
                var genero = dal.RecuperarPor(x => x.Id == id);
                if (genero is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(genero);
                return Results.NoContent();

            });

            app.MapPut("/Generos", ([FromServices] Dal<Genero> dal, GeneroRequestEdit generoRequest) =>
            {
                var generoAtualizado = dal.RecuperarPor(x => x.Id == generoRequest.Id);
                if (generoAtualizado is null)
                {
                    return Results.NotFound();
                }
                generoAtualizado.Nome = generoRequest.Nome;
                generoAtualizado.Descricao = generoRequest.Descricao;
                
                dal.Atualizar(generoAtualizado);

                return Results.Ok();

            });
        }
    }
    
}
