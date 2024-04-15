using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco;

public class DbContextBase // AQUI USO O PACOTE SQLCLIENT PARA FAZER A CONEXÃO COM O BANCO
{
    private string DbConnectionStringPc = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbScreenSound;" +
        "Integrated Security=True;Encrypt=False;" +
        "Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    private string DbConnectionStringNotbook = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbScreenSound;" +
        "Integrated Security=True;Encrypt=False;" +
        "Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public SqlConnection ObterConexao()
    {
        return new SqlConnection(DbConnectionStringPc);
    }

    internal  IEnumerable<Artista> Listar()
    {
        var lista = new List<Artista>();
        using var connection = ObterConexao();
        connection.Open();

        string sql = "Select * from Artistas";
        SqlCommand cmd = new SqlCommand(sql, connection);
        using SqlDataReader dataReader = cmd.ExecuteReader();
        while (dataReader.Read())
        {
            string nomeArtista = Convert.ToString(dataReader["Nome"]);
            string bioArtista = Convert.ToString(dataReader["Bio"]);
            int idArtista = Convert.ToInt32(dataReader["ID"]);

            Artista artista = new(nomeArtista, bioArtista) { Id = idArtista };
            lista.Add(artista);

        }
        return lista;

    }
}
