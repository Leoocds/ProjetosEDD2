using System;
using System.Collections.Generic;
using System.Linq;

class Data
{
    public int Dia { get; set; }
    public int Mes { get; set; }
    public int Ano { get; set; }

    public void SetData(int dia, int mes, int ano)
    {
        Dia = dia;
        Mes = mes;
        Ano = ano;
    }

    public override string ToString()
    {
        return $"{Dia:D2}/{Mes:D2}/{Ano}";
    }
}

class Telefone
{
    public string Tipo { get; set; }
    public string Numero { get; set; }
    public bool Principal { get; set; }
}
class Contato
{
    public string Email { get; set; }
    public string Nome { get; set; }
    public Data DtNasc { get; set; }
    public List<Telefone> Telefones { get; private set; } = new List<Telefone>();

    public int GetIdade()
    {
        var hoje = DateTime.Now;
        var idade = hoje.Year - DtNasc.Ano;
        if (hoje.Month < DtNasc.Mes || (hoje.Month == DtNasc.Mes && hoje.Day < DtNasc.Dia))
        {
            idade--;
        }
        return idade;
    }

    public void AdicionarTelefone(Telefone t)
    {
        Telefones.Add(t);
    }

    public string GetTelefonePrincipal()
    {
        var telefonePrincipal = Telefones.FirstOrDefault(t => t.Principal);
        return telefonePrincipal != null ? telefonePrincipal.Numero : "N/A";
    }

    public override string ToString()
    {
        return $"Nome: {Nome}\nEmail: {Email}\nData de Nascimento: {DtNasc}\nIdade: {GetIdade()} anos\nTelefone Principal: {GetTelefonePrincipal()}";
    }

    public override bool Equals(object obj)
    {
        return obj is Contato contato && Email == contato.Email;
    }

    public override int GetHashCode()
    {
        return Email.GetHashCode();
    }
}

class Contatos
{
    private List<Contato> agenda = new List<Contato>();

    public bool Adicionar(Contato c)
    {
        if (!agenda.Contains(c))
        {
            agenda.Add(c);
            return true;
        }
        return false;
    }

    public Contato Pesquisar(Contato c)
    {
        return agenda.FirstOrDefault(contato => contato.Equals(c));
    }

    public bool Alterar(Contato c)
    {
        var contatoExistente = Pesquisar(c);
        if (contatoExistente != null)
        {
            agenda.Remove(contatoExistente);
            agenda.Add(c);
            return true;
        }
        return false;
    }

    public bool Remover(Contato c)
    {
        return agenda.Remove(Pesquisar(c));
    }

    public void ListarContatos()
    {
        if (agenda.Any())
        {
            foreach (var contato in agenda)
            {
                Console.WriteLine(contato);
                Console.WriteLine("----------------------");
            }
        }
        else
        {
            Console.WriteLine("Nenhum contato cadastrado.");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Contatos agenda = new Contatos();
        int opcao;

        do
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar contato");
            Console.WriteLine("2. Pesquisar contato");
            Console.WriteLine("3. Alterar contato");
            Console.WriteLine("4. Remover contato");
            Console.WriteLine("5. Listar contatos");
            Console.WriteLine("--------------------------------------");
            Console.Write("Escolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    AdicionarContato(agenda);
                    break;
                case 2:
                    PesquisarContato(agenda);
                    break;
                case 3:
                    AlterarContato(agenda);
                    break;
                case 4:
                    RemoverContato(agenda);
                    break;
                case 5:
                    agenda.ListarContatos();
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        } while (opcao != 0);
    }

    static void AdicionarContato(Contatos agenda)
    {
        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine();
        Console.Write("Digite o email: ");
        string email = Console.ReadLine();
        Console.Write("Digite a data de nascimento (dd mm aaaa): ");
        var data = Console.ReadLine().Split(' ');
        Data dtNasc = new Data { Dia = int.Parse(data[0]), Mes = int.Parse(data[1]), Ano = int.Parse(data[2]) };

        Contato contato = new Contato { Nome = nome, Email = email, DtNasc = dtNasc };

        Console.Write("Deseja adicionar um telefone? (s/n): ");
        if (Console.ReadLine().ToLower() == "s")
        {
            Console.Write("Digite o tipo de telefone: ");
            string tipo = Console.ReadLine();
            Console.Write("Digite o número: ");
            string numero = Console.ReadLine();
            Console.Write("É o principal? (s/n): ");
            bool principal = Console.ReadLine().ToLower() == "s";

            Telefone telefone = new Telefone { Tipo = tipo, Numero = numero, Principal = principal };
            contato.AdicionarTelefone(telefone);
        }

        if (agenda.Adicionar(contato))
        {
            Console.WriteLine("Contato adicionado com sucesso!");
        }
        else
        {
            Console.WriteLine("Contato já existe.");
        }
    }

    static void PesquisarContato(Contatos agenda)
    {
        Console.Write("Digite o email do contato: ");
        string email = Console.ReadLine();

        var contato = agenda.Pesquisar(new Contato { Email = email });
        if (contato != null)
        {
            Console.WriteLine("Contato encontrado:");
            Console.WriteLine(contato);
        }
        else
        {
            Console.WriteLine("Contato não encontrado.");
        }
    }

    static void AlterarContato(Contatos agenda)
    {
        Console.Write("Digite o email do contato a ser alterado: ");
        string email = Console.ReadLine();
        var contato = agenda.Pesquisar(new Contato { Email = email });

        if (contato != null)
        {
            Console.Write("Digite o novo nome: ");
            string nome = Console.ReadLine();
            Console.Write("Digite a nova data de nascimento (dd mm aaaa): ");
            var data = Console.ReadLine().Split(' ');
            Data dtNasc = new Data { Dia = int.Parse(data[0]), Mes = int.Parse(data[1]), Ano = int.Parse(data[2]) };

            contato.Nome = nome;
            contato.DtNasc = dtNasc;

            agenda.Alterar(contato);
            Console.WriteLine("Contato alterado com sucesso!");
        }
        else
        {
            Console.WriteLine("Contato não encontrado.");
        }
    }

    static void RemoverContato(Contatos agenda)
    {
        Console.Write("Digite o email do contato a ser removido: ");
        string email = Console.ReadLine();

        if (agenda.Remover(new Contato { Email = email }))
        {
            Console.WriteLine("Contato removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Contato não encontrado.");
        }
    }
}
