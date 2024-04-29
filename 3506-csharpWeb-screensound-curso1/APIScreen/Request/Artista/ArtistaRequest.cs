using System.ComponentModel.DataAnnotations;

namespace APIScreen.Request.Artista
{
    public class ArtistaRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Bio { get; set; }
    }
}
