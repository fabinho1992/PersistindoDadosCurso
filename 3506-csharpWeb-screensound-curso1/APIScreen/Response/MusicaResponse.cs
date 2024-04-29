namespace APIScreen.Response
{
    public class MusicaResponse
    {
        public MusicaResponse(int id, string nome, string nomeArtista)
        {
            Id = id;
            Nome = nome;
            NomeArtista = nomeArtista;
        }

        public int Id { get; set; }
        public string Nome { get; set; } 
        public string NomeArtista { get; set; }
    }
}
