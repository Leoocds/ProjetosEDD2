using ProjVendedores;

class Program
{
    static void Main(string[] args)
    {
        Vendedores vendedores = new Vendedores();

        while (true)
        {
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Cadastrar vendedor");
            Console.WriteLine("2. Consultar vendedor");
            Console.WriteLine("3. Excluir vendedor");
            Console.WriteLine("4. Registrar venda");
            Console.WriteLine("5. Listar vendedores");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Saindo...");
                    return;

                case 1:
                    Console.Write("ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();
                    Console.Write("Percentual de comissão (em decimal, ex: 0.1): ");
                    double percComissao = double.Parse(Console.ReadLine());

                    Vendedor vendedor = new Vendedor(id, nome, percComissao);
                    if (vendedores.AddVendedor(vendedor))
                    {
                        Console.WriteLine("Vendedor cadastrado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Limite de vendedores atingido.");
                    }
                    break;

                case 2:
                    Console.Write("ID do vendedor: ");
                    int idConsulta = int.Parse(Console.ReadLine());
                    var vendedorConsulta = vendedores.SearchVendedor(idConsulta);

                    if (vendedorConsulta != null)
                    {
                        Console.WriteLine($"ID: {vendedorConsulta.Id}");
                        Console.WriteLine($"Nome: {vendedorConsulta.Nome}");
                        Console.WriteLine($"Valor total das vendas: {vendedorConsulta.ValorVendas():C}");
                        Console.WriteLine($"Comissão: {vendedorConsulta.ValorComissao():C}");
                        Console.WriteLine($"Média diária de vendas: {vendedorConsulta.ValorMedioVendas():C}");
                    }
                    else
                    {
                        Console.WriteLine("Vendedor não encontrado.");
                    }
                    break;

                case 3:
                    Console.Write("ID do vendedor a ser excluído: ");
                    int idExcluir = int.Parse(Console.ReadLine());
                    var vendedorExcluir = vendedores.SearchVendedor(idExcluir);

                    if (vendedorExcluir != null && vendedores.DelVendedor(vendedorExcluir))
                    {
                        Console.WriteLine("Vendedor excluído com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine("Vendedor não encontrado ou possui vendas registradas.");
                    }
                    break;

                case 4:
                    Console.Write("ID do vendedor: ");
                    int idVenda = int.Parse(Console.ReadLine());
                    var vendedorVenda = vendedores.SearchVendedor(idVenda);

                    if (vendedorVenda != null)
                    {
                        Console.Write("Dia da venda (1-31): ");
                        int dia = int.Parse(Console.ReadLine());
                        Console.Write("Quantidade: ");
                        int qtde = int.Parse(Console.ReadLine());
                        Console.Write("Valor total: ");
                        double valor = double.Parse(Console.ReadLine());

                        vendedorVenda.RegistrarVenda(dia, new Venda(qtde, valor));
                        Console.WriteLine("Venda registrada com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine("Vendedor não encontrado.");
                    }
                    break;

                case 5:
                    Console.WriteLine("Listando vendedores...");
                    double totalVendas = 0;
                    double totalComissoes = 0;

                    foreach (var v in vendedores.OsVendedores.Where(v => v != null))
                    {
                        Console.WriteLine($"ID: {v.Id}, Nome: {v.Nome}, Vendas: {v.ValorVendas():C}, Comissão: {v.ValorComissao():C}");
                        totalVendas += v.ValorVendas();
                        totalComissoes += v.ValorComissao();
                    }

                    Console.WriteLine($"Total de vendas: {totalVendas:C}");
                    Console.WriteLine($"Total de comissões: {totalComissoes:C}");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
