using System;
using System.Collections.Generic;

namespace ProjetoMedicamento
{
    public class Lote
    {
        public int Id { get; set; }
        public int Qtde { get; set; }
        public DateTime Venc { get; set; }

        public Lote(int id, int qtde, DateTime venc)
        {
            Id = id;
            Qtde = qtde;
            Venc = venc;
        }

        public override string ToString()
        {
            return $"{Id} \t {Qtde} \t {Venc.ToString("dd/MM/yyyy")}";
        }
    }

    public class Medicamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Laboratorio { get; set; }
        public Queue<Lote> Lotes { get; set; } = new Queue<Lote>();

        public Medicamento(int id, string nome, string laboratorio)
        {
            Id = id;
            Nome = nome;
            Laboratorio = laboratorio;
        }

        public int QtdeDisponivel()
        {
            int total = 0;
            foreach (var lote in Lotes)
            {
                total += lote.Qtde;
            }
            return total;
        }

        public void Comprar(Lote lote)
        {
            Lotes.Enqueue(lote);
        }

        public bool Vender(int qtde)
        {
            int qtdeRestante = qtde;

            while (qtdeRestante > 0 && Lotes.Count > 0)
            {
                var loteAtual = Lotes.Peek();

                if (loteAtual.Qtde <= qtdeRestante)
                {
                    qtdeRestante -= loteAtual.Qtde;
                    Lotes.Dequeue();
                }
                else
                {
                    loteAtual.Qtde -= qtdeRestante;
                    qtdeRestante = 0;
                }
            }

            return qtdeRestante == 0;
        }

        public override string ToString()
        {
            return $"{Id} \t {Nome} \t {Laboratorio} \t {QtdeDisponivel()}";
        }

        public override bool Equals(object obj)
        {
            return obj is Medicamento medicamento && Id == medicamento.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class Medicamentos
    {
        private List<Medicamento> ListaMedicamentos { get; set; } = new List<Medicamento>();

        public void Adicionar(Medicamento medicamento)
        {
            ListaMedicamentos.Add(medicamento);
        }

        public bool Deletar(Medicamento medicamento)
        {
            return ListaMedicamentos.Remove(medicamento);
        }

        public Medicamento Pesquisar(Medicamento medicamento)
        {
            return ListaMedicamentos.Find(m => m.Equals(medicamento));
        }

        public List<Medicamento> Listar()
        {
            return ListaMedicamentos;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Medicamentos medicamentos = new Medicamentos();
            int opcao;

            do
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1. Cadastrar medicamento");
                Console.WriteLine("2. Consultar medicamento (sintético)");
                Console.WriteLine("3. Consultar medicamento (analítico)");
                Console.WriteLine("4. Comprar medicamento");
                Console.WriteLine("5. Vender medicamento");
                Console.WriteLine("6. Sair");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Write("ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();
                        Console.Write("Laboratório: ");
                        string laboratorio = Console.ReadLine();

                        medicamentos.Adicionar(new Medicamento(id, nome, laboratorio));
                        break;

                    case 2:
                        Console.Write("ID do medicamento: ");
                        int idPesquisa = int.Parse(Console.ReadLine());
                        Medicamento medPesquisa = new Medicamento(idPesquisa, "", "");
                        Medicamento encontrado = medicamentos.Pesquisar(medPesquisa);

                        if (encontrado != null)
                        {
                            Console.WriteLine(encontrado);
                        }
                        else
                        {
                            Console.WriteLine("Medicamento não encontrado.");
                        }
                        break;

                    case 3:
                        Console.Write("ID do medicamento: ");
                        int idAnalitico = int.Parse(Console.ReadLine());
                        Medicamento medAnalitico = new Medicamento(idAnalitico, "", "");
                        Medicamento encontradoAnalitico = medicamentos.Pesquisar(medAnalitico);

                        if (encontradoAnalitico != null)
                        {
                            Console.WriteLine(encontradoAnalitico);
                            foreach (var lote in encontradoAnalitico.Lotes)
                            {
                                Console.WriteLine(lote);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Medicamento não encontrado.");
                        }
                        break;

                    case 4:
                        Console.Write("ID do medicamento: ");
                        int idCompra = int.Parse(Console.ReadLine());
                        Console.Write("Quantidade: ");
                        int qtdeCompra = int.Parse(Console.ReadLine());
                        Console.Write("Data de vencimento (yyyy-mm-dd): ");
                        
                        DateTime vencimento;
                        bool dataValida = DateTime.TryParse(Console.ReadLine(), out vencimento);

                        if (!dataValida)
                        {
                            Console.WriteLine("Erro: Formato de data inválido.");
                            break;
                        }

                        Medicamento medCompra = new Medicamento(idCompra, "", "");
                        Medicamento encontradoCompra = medicamentos.Pesquisar(medCompra);

                        if (encontradoCompra != null)
                        {
                            encontradoCompra.Comprar(new Lote(idCompra, qtdeCompra, vencimento));
                            Console.WriteLine("Lote adicionado com sucesso.");
                        }
                        else
                        {
                            Console.WriteLine("Medicamento não encontrado.");
                        }
                        break;

                    case 5:
                        Console.Write("ID do medicamento: ");
                        int idVenda = int.Parse(Console.ReadLine());
                        Console.Write("Quantidade: ");
                        int qtdeVenda = int.Parse(Console.ReadLine());

                        Medicamento medVenda = new Medicamento(idVenda, "", "");
                        Medicamento encontradoVenda = medicamentos.Pesquisar(medVenda);

                        if (encontradoVenda != null)
                        {
                            if (encontradoVenda.Vender(qtdeVenda))
                            {
                                Console.WriteLine("Venda realizada com sucesso.");
                            }
                            else
                            {
                                Console.WriteLine("Não há estoque suficiente para a venda.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Medicamento não encontrado.");
                        }
                        break;
                }

            } while (opcao != 6);
        }
    }
}
