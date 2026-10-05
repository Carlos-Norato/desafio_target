using desafio_target.Models;
using desafio_target.Models.Enumerables;
using desafio_target.Services;
using System.Text.Json;
using System.Text.Json.Nodes;

while (true)
{
    Console.WriteLine("------------------------------");
    Console.WriteLine("DESAFIO TARGET");
    Console.WriteLine("------------------------------");
    Console.WriteLine("1 - Calcular comissão"); 
    Console.WriteLine("2 - Movimentar estoque");
    Console.WriteLine("3 - Calcular juros");
    Console.WriteLine("0 - Sair");
    Console.WriteLine();

    Console.Write("Escolha uma opção: ");

    var opcao = Console.ReadLine();


    switch (opcao)
    {
        case "1":
            ExecutarComissao();
            break;
        
        case "2":
            ExecutarEstoque();
            break;

        case "3":
            ExecutarJuros();
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

static void ExecutarEstoque()
{
    var estoqueService = new EstoqueService();

    var json = File.ReadAllText("Data/estoque.json");

    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };
    var estoque = JsonNode.Parse(json)?["estoque"].Deserialize<Produto[]>(options);
    
    while (true)
    {
        Console.Clear();
        Console.WriteLine("------------------------------");
        Console.WriteLine("Movimentação de Estoque");
        Console.WriteLine("------------------------------");
        Console.WriteLine("1 - Movimentar produto");
        Console.WriteLine("2 - Listar estoque");
        Console.WriteLine("3 - Listar movimentações");
        Console.WriteLine("0 - Volta ao menu inicial");
        Console.WriteLine();

        Console.Write("Escolha uma opção: ");

        var opcao = Console.ReadLine();


        try
        {
            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Digite o código do produto: ");
                    int codigoProduto = int.TryParse(Console.ReadLine(), out int parsedCodigo) ? parsedCodigo : throw new ArgumentException("Código inválido.");
                    Produto? produtoEstoque = estoque.FirstOrDefault(p => p.Codigo == codigoProduto);
                    if (produtoEstoque is null)
                        throw new ArgumentException("Produto não encontrado.");

                    Console.Write("Escolha o tipo de movimentação (1 - Entrada, 2 - Saída): ");
                    TipoMovimentacaoEnum tipoMovimentacao = Enum.TryParse<TipoMovimentacaoEnum>(Console.ReadLine(), out var parsedTipo) 
                                                            && Enum.IsDefined(typeof(TipoMovimentacaoEnum), parsedTipo) ? parsedTipo : throw new ArgumentException("Tipo de movimentação inválido.");

                    Console.Write("Digite a quantidade: ");
                    int quantidade = int.TryParse(Console.ReadLine(), out int parsedQuantidade) ? parsedQuantidade : throw new ArgumentException("Quantidade inválida.");

                    int quantidadeFinal = estoqueService.MovimentarProduto(produtoEstoque, tipoMovimentacao, quantidade);
                    Console.WriteLine($"Quantidade final do produto {produtoEstoque.Descricao}: {quantidadeFinal}");
                    Console.WriteLine("Pressione ENTER para continuar...");
                    Console.ReadLine();
                    break;

                case "2":   
                    Console.Clear();
                    foreach (var produto in estoque)
                    {
                        Console.WriteLine($"Código do produto: {produto.Codigo}");
                        Console.WriteLine($"Nome do produto: {produto.Descricao}");
                        Console.WriteLine($"Quantidade do produto: {produto.Estoque}");
                        Console.WriteLine();
                    }

                    Console.WriteLine("Pressione ENTER para continuar...");
                    Console.ReadLine();
                    break;

                case "3":
                    Console.Clear();
                    var movimentacoes = estoqueService.ListarMovimentacoes();
                    foreach (var movimentacao in movimentacoes)
                    {
                        Console.WriteLine($"Código da movimentação: {movimentacao.Codigo}");
                        Console.WriteLine($"Código do produto: {movimentacao.CodigoProduto}");
                        Console.WriteLine($"Quantidade: {movimentacao.Quantidade}");
                        Console.WriteLine($"Tipo: {movimentacao.TipoMovimentacao}");
                        Console.WriteLine();
                    }
                    Console.WriteLine("Pressione ENTER para continuar...");
                    Console.ReadLine();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}

static void ExecutarJuros()
{
    var jurosService = new JurosService();

    while (true)
    {
        Console.Clear();
        Console.WriteLine("------------------------------");
        Console.WriteLine("Cálculo de Juros");
        Console.WriteLine("------------------------------");
        Console.WriteLine("1 - Calcular juros simples");
        Console.WriteLine("2 - Calcular juros composto");
        Console.WriteLine("0 - Volta ao menu inicial");
        Console.WriteLine();

        Console.Write("Escolha uma opção: ");

        var opcao = Console.ReadLine();

        try
        {
            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Digite o valor: ");
                    decimal valorSimples = decimal.TryParse(Console.ReadLine(), out decimal parsedValorSimples) ? parsedValorSimples : throw new ArgumentException("Valor inválido.");

                    Console.Write("Digite a data de vencimento (dd/mm/yyyy): ");
                    DateTime dataVencimentoSimples = DateTime.TryParse(Console.ReadLine(), out DateTime parsedDataVencimentoSimples) ? parsedDataVencimentoSimples : throw new ArgumentException("Data inválida.");

                    decimal valorJurosSimples = jurosService.CalcularJurosSimples(valorSimples, dataVencimentoSimples);
                    Console.WriteLine($"Juros: {valorJurosSimples:C2}");
                    Console.WriteLine($"Valor total: {(valorSimples + valorJurosSimples):C2}");

                    Console.WriteLine();
                    Console.WriteLine("Pressione ENTER para continuar..."); 
                    Console.ReadLine();
                    break;

                case "2":
                    Console.Clear();
                    Console.Write("Digite o valor: ");
                    decimal valorComposto = decimal.TryParse(Console.ReadLine(), out decimal parsedValorComposto) ? parsedValorComposto : throw new ArgumentException("Valor inválido.");

                    Console.Write("Digite a data de vencimento (dd/mm/yyyy): ");
                    DateTime dataVencimentoComposto = DateTime.TryParse(Console.ReadLine(), out DateTime parsedDataVencimentoComposto) ? parsedDataVencimentoComposto : throw new ArgumentException("Data inválida.");

                    decimal valorJurosComposto = jurosService.CalcularJurosComposto(valorComposto, dataVencimentoComposto);
                    Console.WriteLine($"Juros: {valorJurosComposto:C2}");
                    Console.WriteLine($"Valor total: {(valorComposto + valorJurosComposto):C2}");

                    Console.WriteLine();
                    Console.WriteLine("Pressione ENTER para continuar...");
                    Console.ReadLine();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
        }
    }
}