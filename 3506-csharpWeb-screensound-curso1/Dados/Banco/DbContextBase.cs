using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ScreenSound.Banco;

public class DbContextBase : DbContext// AQUI USO O PACOTE SQLCLIENT PARA FAZER A CONEXÃO COM O BANCO
{
    public DbSet<Artista> Artistas { get; set; }
    public DbSet<Musica> Musicas { get; set; }

    private string DbConnectionStringPc = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbScreenSoundV0;" +
        "Integrated Security=True;Encrypt=False;" +
        "Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.
            UseSqlServer(DbConnectionStringPc)
            .UseLazyLoadingProxies();
    }


    

   

    
}
