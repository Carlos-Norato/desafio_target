using desafio_target.Models;
using desafio_target.Services;
using System.Text.Json;
using System.Text.Json.Nodes;

while (true)
{
    Console.WriteLine("------------------------------");
    Console.WriteLine("DESAFIO TARGET");
    Console.WriteLine("------------------------------");
    Console.WriteLine("1 - Calcular comissão");
    Console.WriteLine("0 - Sair");
    Console.WriteLine();

    Console.Write("Escolha uma opção: ");

    var opcao = Console.ReadLine();


    switch (opcao)
    {
        case "1":
            ExecutarComissao();
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Opção inválida.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Pressione ENTER para continuar...");
    Console.ReadLine();
}


static void ExecutarComissao()
{
    Console.Clear();
    Console.WriteLine("------------------------------");
    Console.WriteLine("Cálculo de Comissões");
    Console.WriteLine("------------------------------");
    Console.WriteLine();

    var json = File.ReadAllText("Data/vendas.json");
    
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
    var vendas = JsonNode.Parse(json)?["vendas"].Deserialize<Venda[]>(options);

    if (vendas is null)
    {
        Console.WriteLine("Não foi possível carregar os dados.");
        return;
    }

    var comissaoService = new ComissaoService();

    var comissoes = comissaoService.CalcularComissoes(vendas);

    foreach (var (vendedor, comissao) in comissoes)
    {
        Console.WriteLine(
            $"{vendedor}: {comissao:C2}");
    }
}