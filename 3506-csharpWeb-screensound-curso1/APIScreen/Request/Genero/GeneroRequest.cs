using System.ComponentModel.DataAnnotations;

namespace APIScreen.Request.Genero
{
    public class GeneroRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }

    }
}
