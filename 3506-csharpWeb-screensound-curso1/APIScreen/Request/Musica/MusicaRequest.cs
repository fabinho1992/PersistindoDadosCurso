using APIScreen.Request.Genero;
using System.ComponentModel.DataAnnotations;

namespace APIScreen.Request.Musica
{
    public class MusicaRequest
    {

        [Required]
        public string Nome { get; set; }
        [Required]
        public int? AnoLancamento { get; set; }
        [Required]
        public int ArtistaId { get; set; }
        [Required]
        public ICollection<GeneroRequest> Generos { get; set; } = null;
    }
}
