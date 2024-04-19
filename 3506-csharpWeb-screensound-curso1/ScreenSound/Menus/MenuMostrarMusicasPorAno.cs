using ScreenSound.Banco;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Menus
{
    internal class MenuMostrarMusicasPorAno : Menu
    {
        public override void Executar(Dal<Artista> dal)
        {
            base.Executar(dal);
            ExibirTituloDaOpcao("Exibir Musicas pelo ano de lançamento");
            Console.Write("Digite o ano que quer consultar: ");
            string anoMusicas = Console.ReadLine()!;
            var musicaDal = new Dal<Musica>( new DbContextBase ());
            var anoLancamento = musicaDal.ListarPor(x => x.AnoLancamento == Convert.ToInt32(anoMusicas)); 
            if (anoLancamento != null)
            {
                foreach (var musica in anoLancamento)
                {
                    musica.ExibirFichaTecnica();    
                }
                
                Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"\nNenhuma musica foi encontrado!");
                Console.WriteLine("Digite uma tecla para voltar ao menu principal");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
