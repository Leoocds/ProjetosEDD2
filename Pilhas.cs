using System;
using System.Collections.Generic;

namespace TransporteFrota
{
    class Veiculo
    {
        public int Id { get; set; }
        public int Capacidade { get; set; }
        public int PassageirosTransportados { get; private set; }

        public Veiculo(int id, int capacidade)
        {
            Id = id;
            Capacidade = capacidade;
            PassageirosTransportados = 0;
        }

        public void AdicionarPassageiros(int quantidade)
        {
            PassageirosTransportados += quantidade;
        }

        public void LimparEstatisticas()
        {
            PassageirosTransportados = 0;
        }
    }

    class Garagem
    {
        public int Id { get; set; }
        public string Localizacao { get; set; }
        private Stack<Veiculo> Veiculos { get; set; }

        public Garagem(int id, string localizacao)
        {
            Id = id;
            Localizacao = localizacao;
            Veiculos = new Stack<Veiculo>();
        }

        public void AdicionarVeiculo(Veiculo veiculo)
        {
            Veiculos.Push(veiculo);
        }

        public Veiculo RemoverVeiculo()
        {
            if (Veiculos.Count > 0)
                return Veiculos.Pop();

            throw new InvalidOperationException("Não há veículos disponíveis na garagem.");
        }

        public void ListarVeiculos()
        {
            Console.WriteLine($"Garagem {Localizacao} contém {Veiculos.Count} veículo(s):");
            foreach (var veiculo in Veiculos)
            {
                Console.WriteLine($"- Veículo ID: {veiculo.Id}, Capacidade: {veiculo.Capacidade}");
            }
        }

        public int QuantidadeVeiculos() => Veiculos.Count;
    }

    class Viagem
    {
        public Garagem Origem { get; set; }
        public Garagem Destino { get; set; }
        public Veiculo Veiculo { get; set; }
        public int QuantidadePassageiros { get; set; }

        public Viagem(Garagem origem, Garagem destino, Veiculo veiculo, int passageiros)
        {
            Origem = origem;
            Destino = destino;
            Veiculo = veiculo;
            QuantidadePassageiros = passageiros;
        }
    }

    class SistemaFrota
    {
        private List<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
        private List<Garagem> Garagens { get; set; } = new List<Garagem>();
        private List<Viagem> Viagens { get; set; } = new List<Viagem>();

        public void CadastrarVeiculo(int id, int capacidade)
        {
            Veiculos.Add(new Veiculo(id, capacidade));
            Console.WriteLine($"Veículo ID {id} cadastrado com sucesso.");
        }

        public void CadastrarGaragem(int id, string localizacao)
        {
            Garagens.Add(new Garagem(id, localizacao));
            Console.WriteLine($"Garagem {localizacao} cadastrada com sucesso.");
        }

        public void IniciarJornada()
        {
            Console.WriteLine("Iniciando jornada...");
            for (int i = 0; i < Veiculos.Count; i++)
            {
                Garagens[i % Garagens.Count].AdicionarVeiculo(Veiculos[i]);
            }
            Console.WriteLine("Jornada iniciada com sucesso.");
        }

        public void EncerrarJornada()
        {
            Console.WriteLine("Encerrando jornada...");
            foreach (var veiculo in Veiculos)
            {
                veiculo.LimparEstatisticas();
            }
            Viagens.Clear();
            Console.WriteLine("Jornada encerrada e estatísticas limpas.");
        }

        public void LiberarViagem(int origemId, int destinoId, int passageiros)
        {
            var origem = Garagens.Find(g => g.Id == origemId);
            var destino = Garagens.Find(g => g.Id == destinoId);

            if (origem == null || destino == null)
            {
                Console.WriteLine("Origem ou destino inválidos.");
                return;
            }

            try
            {
                var veiculo = origem.RemoverVeiculo();
                veiculo.AdicionarPassageiros(passageiros);
                destino.AdicionarVeiculo(veiculo);

                Viagens.Add(new Viagem(origem, destino, veiculo, passageiros));
                Console.WriteLine($"Viagem liberada de {origem.Localizacao} para {destino.Localizacao}.");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ListarVeiculos(int garagemId)
        {
            var garagem = Garagens.Find(g => g.Id == garagemId);
            if (garagem != null)
                garagem.ListarVeiculos();
            else
                Console.WriteLine("Garagem não encontrada.");
        }

        public void ListarViagens(int origemId, int destinoId)
        {
            var viagens = Viagens.FindAll(v => v.Origem.Id == origemId && v.Destino.Id == destinoId);

            Console.WriteLine($"Viagens de origem {origemId} para destino {destinoId}:");
            foreach (var viagem in viagens)
            {
                Console.WriteLine($"- Veículo ID {viagem.Veiculo.Id}, Passageiros: {viagem.QuantidadePassageiros}");
            }
        }

        public int QuantidadePassageirosTransportados(int origemId, int destinoId)
        {
            int total = 0;
            foreach (var viagem in Viagens)
            {
                if (viagem.Origem.Id == origemId && viagem.Destino.Id == destinoId)
                {
                    total += viagem.QuantidadePassageiros;
                }
            }
            return total;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SistemaFrota sistema = new SistemaFrota();

            while (true)
            {
                Console.WriteLine("\nMenu de Opções:");
                Console.WriteLine("0 - Finalizar");
                Console.WriteLine("1 - Cadastrar veículo");
                Console.WriteLine("2 - Cadastrar garagem");
                Console.WriteLine("3 - Iniciar jornada");
                Console.WriteLine("4 - Encerrar jornada");
                Console.WriteLine("5 - Liberar viagem");
                Console.WriteLine("6 - Listar veículos em garagem");
                Console.WriteLine("7 - Listar viagens entre origens/destinos");
                Console.WriteLine("8 - Quantidade de passageiros transportados");

                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando o programa...");
                        return;

                    case 1:
                        Console.WriteLine("Digite o ID e a capacidade do veículo:");
                        int id = int.Parse(Console.ReadLine());
                        int capacidade = int.Parse(Console.ReadLine());
                        sistema.CadastrarVeiculo(id, capacidade);
                        break;

                    case 2:
                        Console.WriteLine("Digite o ID e a localização da garagem:");
                        int garagemId = int.Parse(Console.ReadLine());
                        string localizacao = Console.ReadLine();
                        sistema.CadastrarGaragem(garagemId, localizacao);
                        break;

                    case 3:
                        sistema.IniciarJornada();
                        break;

                    case 4:
                        sistema.EncerrarJornada();
                        break;

                    case 5:
                        Console.WriteLine("Digite a origem, destino e o número de passageiros:");
                        int origemId = int.Parse(Console.ReadLine());
                        int destinoId = int.Parse(Console.ReadLine());
                        int passageiros = int.Parse(Console.ReadLine());
                        sistema.LiberarViagem(origemId, destinoId, passageiros);
                        break;

                    case 6:
                        Console.WriteLine("Digite o ID da garagem:");
                        int listarGaragemId = int.Parse(Console.ReadLine());
                        sistema.ListarVeiculos(listarGaragemId);
                        break;

                    case 7:
                        Console.WriteLine("Digite a origem e destino para listar viagens:");
                        int origemListarId = int.Parse(Console.ReadLine());
                        int destinoListarId = int.Parse(Console.ReadLine());
                        sistema.ListarViagens(origemListarId, destinoListarId);
                        break;

                    case 8:
                        Console.WriteLine("Digite a origem e destino para total de passageiros:");
                        int origemPassageiros = int.Parse(Console.ReadLine());
                        int destinoPassageiros = int.Parse(Console.ReadLine());
                        int total = sistema.QuantidadePassageirosTransportados(origemPassageiros, destinoPassageiros);
                        Console.WriteLine($"Total de passageiros transportados: {total}");
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }
    }
}
