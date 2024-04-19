using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuRegistrarMusica : Menu
{
    public override void Executar(Dal<Artista> artistaDal)
    {
        base.Executar(artistaDal);
        ExibirTituloDaOpcao("Registro de músicas");
        Console.Write("Digite o artista cuja música deseja registrar: ");
        string nomeDoArtista = Console.ReadLine()!;
        var artista = artistaDal.RecuperarPor(x => x.Nome == nomeDoArtista);
        if (artista != null)
        {
            Console.Write("Agora digite o título da música: ");
            string tituloDaMusica = Console.ReadLine()!;
            Console.Write("Agora digite o anoo de lançamento da música: ");
            var anoLancamento = Console.ReadLine()!;
            artista.AdicionarMusica(new Musica(tituloDaMusica) { AnoLancamento = Convert.ToInt32(anoLancamento)});
            Console.WriteLine($"A música {tituloDaMusica} de {nomeDoArtista} foi registrada com sucesso!");
            artistaDal.Atualizar(artista);
            Thread.Sleep(4000);
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
