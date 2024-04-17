//using Microsoft.Data.SqlClient;
//using ScreenSound.Modelos;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ScreenSound.Banco
//{
//    public class ArtistaDal : Dal<Artista>
//    {
        
//        public ArtistaDal(DbContextBase dbContext) : base(dbContext) { }
        
//        //Aqui está um exemplo doo uso do ADO.Net, para acessar o banco de dados
//        //public IEnumerable<Artista> Listar()
//        //{

//            //Método usando ADO.Net

//            //var lista = new List<Artista>();
//            //using var connection = new DbContextBase().ObterConexao();
//            //connection.Open();

//            //string sql = "Select * from Artistas";
//            //SqlCommand command = new SqlCommand(sql, connection);
//            //using SqlDataReader dataReader = command.ExecuteReader();
//            //while (dataReader.Read())
//            //{
//            //    string nomeArtista = Convert.ToString(dataReader["Nome"]);
//            //    string bioArtista = Convert.ToString(dataReader["Bio"]);
//            //    int idArtista = Convert.ToInt32(dataReader["ID"]);

//            //    Artista artista = new(nomeArtista, bioArtista) { Id = idArtista };
//            //    lista.Add(artista);

//            //}
//            //return lista;
//        //}

        

//        //public void Adicionar(Artista artista)
//        //{
//        //    _dbContext.Artistas.Add(artista);
//        //    _dbContext.SaveChanges();

//        //    //using var connection = new DbContextBase().ObterConexao();
//        //    //connection.Open();

//        //    //string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
//        //    //SqlCommand command = new SqlCommand(sql, connection);

//        //    //command.Parameters.AddWithValue("@nome", artista.Nome);
//        //    //command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
//        //    //command.Parameters.AddWithValue("@bio", artista.Bio);

//        //    //int retorno = command.ExecuteNonQuery();
//        //    //Console.WriteLine($"Linhas afetadas: {retorno}");
//        //}

//        //public void Deletar(Artista artista)
//        //{
//        //    _dbContext.Artistas.Remove(artista);
//        //    _dbContext.SaveChanges();

//        //    //using var connection = new DbContextBase().ObterConexao();
//        //    //connection.Open();

//        //    //string sql = $"DELETE FROM Artistas WHERE Id = @id";
//        //    //SqlCommand command = new SqlCommand(sql, connection);

//        //    //command.Parameters.AddWithValue("@id", artista.Id);

//        //    //var retorno = command.ExecuteNonQuery();
//        //    //Console.WriteLine($"Linhas afetadas: {retorno}");

//        //}

//        //public void Atualizar(Artista artista)
//        //{
//        //    _dbContext.Artistas.Update(artista);
//        //    _dbContext.SaveChanges();

//        //    //using var connection = new DbContextBase().ObterConexao();
//        //    //connection.Open();

//        //    //string sql = $"UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id";
//        //    //SqlCommand command = new SqlCommand(sql, connection);

//        //    //command.Parameters.AddWithValue("@nome", artista.Nome);
//        //    //command.Parameters.AddWithValue("@id", artista.Id);
//        //    //command.Parameters.AddWithValue("@bio", artista.Bio);

//        //    //int retorno = command.ExecuteNonQuery();
//        //    //Console.WriteLine($"Linhas afetadas: {retorno}");
//        //}


//    }
//}

