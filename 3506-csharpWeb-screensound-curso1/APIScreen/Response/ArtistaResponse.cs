namespace APIScreen.Response
{
    public class ArtistaResponse
    {
        public ArtistaResponse(int id, string nome, string fotoPerfil, string bio)
        {
            Id = id;
            Nome = nome;
            FotoPerfil = fotoPerfil;
            Bio = bio;
        }

        public int Id { get; set; }
        public string Nome { get; set; } 
        public string FotoPerfil { get; set; } 
        public string Bio { get; set; } 
        
    }
}
