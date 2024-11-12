using System;
using System.Collections.Generic;
using System.Linq;

class Emprestimo
{
    public DateTime DtEmprestimo { get; set; }
    public DateTime DtDevolucao { get; set; }

    public Emprestimo() => DtEmprestimo = DateTime.Now;
}

class Exemplar
{
    public int Tombo { get; }
    public List<Emprestimo> Emprestimos { get; } = new List<Emprestimo>();

    public Exemplar(int tombo) => Tombo = tombo;

    public bool Emprestar()
    {
        if (Disponivel())
        {
            Emprestimos.Add(new Emprestimo());
            return true;
        }
        return false;
    }

    public bool Devolver()
    {
        var emprestimo = Emprestimos.LastOrDefault(e => e.DtDevolucao == DateTime.MinValue);
        if (emprestimo != null)
        {
            emprestimo.DtDevolucao = DateTime.Now;
            return true;
        }
        return false;
    }

    public bool Disponivel() => Emprestimos.All(e => e.DtDevolucao != DateTime.MinValue);
    public int QtdeEmprestimos() => Emprestimos.Count;
}

class Livro
{
    public int Isbn { get; }
    public string Titulo { get; }
    public string Autor { get; }
    public string Editora { get; }
    public List<Exemplar> Exemplares { get; } = new List<Exemplar>();

    public Livro(int isbn, string titulo, string autor, string editora)
    {
        Isbn = isbn;
        Titulo = titulo;
        Autor = autor;
        Editora = editora;
    }

    public void AdicionarExemplar(Exemplar exemplar) => Exemplares.Add(exemplar);
    public int QtdeExemplares() => Exemplares.Count;
    public int QtdeDisponiveis() => Exemplares.Count(e => e.Disponivel());
    public int QtdeEmprestimos() => Exemplares.Sum(e => e.QtdeEmprestimos());
    public double PercDisponibilidade() => QtdeExemplares() > 0 ? (double)QtdeDisponiveis() / QtdeExemplares() * 100 : 0;
}

class Livros
{
    private List<Livro> acervo = new List<Livro>();

    public void Adicionar(Livro livro) => acervo.Add(livro);
    public Livro Pesquisar(int isbn) => acervo.FirstOrDefault(l => l.Isbn == isbn);
}

class Program
{
    static void Main()
    {
        Livros acervo = new Livros();
        while (true)
        {
            Console.WriteLine("0. Sair\n1. Adicionar livro\n2. Pesquisar livro (sintético)\n3. Pesquisar livro (analítico)\n4. Adicionar exemplar\n5. Registrar empréstimo\n6. Registrar devolução");
            int opcao = int.Parse(Console.ReadLine());

            if (opcao == 0) break;

            if (opcao == 1)
            {
                Console.Write("ISBN: "); int isbn = int.Parse(Console.ReadLine());
                Console.Write("Título: "); string titulo = Console.ReadLine();
                Console.Write("Autor: "); string autor = Console.ReadLine();
                Console.Write("Editora: "); string editora = Console.ReadLine();
                acervo.Adicionar(new Livro(isbn, titulo, autor, editora));
            }
            else if (opcao == 2)
            {
                Console.Write("ISBN: "); int isbn = int.Parse(Console.ReadLine());
                var livro = acervo.Pesquisar(isbn);
                if (livro != null)
                    Console.WriteLine($"Título: {livro.Titulo}, Autor: {livro.Autor}, Exemplares: {livro.QtdeExemplares()}, Disponíveis: {livro.QtdeDisponiveis()}, Empréstimos: {livro.QtdeEmprestimos()}, Disponibilidade: {livro.PercDisponibilidade():F2}%");
            }
            else if (opcao == 3)
            {
                Console.Write("ISBN: "); int isbn = int.Parse(Console.ReadLine());
                var livro = acervo.Pesquisar(isbn);
                if (livro != null)
                {
                    Console.WriteLine($"Título: {livro.Titulo}, Autor: {livro.Autor}, Exemplares: {livro.QtdeExemplares()}, Disponíveis: {livro.QtdeDisponiveis()}, Empréstimos: {livro.QtdeEmprestimos()}, Disponibilidade: {livro.PercDisponibilidade():F2}%");
                    foreach (var exemplar in livro.Exemplares)
                    {
                        Console.WriteLine($"  Tombo: {exemplar.Tombo}, Disponível: {exemplar.Disponivel()}, Empréstimos: {exemplar.QtdeEmprestimos()}");
                        foreach (var emprestimo in exemplar.Emprestimos)
                            Console.WriteLine($"    Empréstimo: {emprestimo.DtEmprestimo}, Devolução: {(emprestimo.DtDevolucao == DateTime.MinValue ? "Não devolvido" : emprestimo.DtDevolucao.ToString())}");
                    }
                }
            }
            else if (opcao == 4)
            {
                Console.Write("ISBN: "); int isbn = int.Parse(Console.ReadLine());
                var livro = acervo.Pesquisar(isbn);
                if (livro != null)
                {
                    Console.Write("Tombo: "); int tombo = int.Parse(Console.ReadLine());
                    livro.AdicionarExemplar(new Exemplar(tombo));
                }
            }
            else if (opcao == 5)
            {
                Console.Write("ISBN: "); int isbn = int.Parse(Console.ReadLine());
                var livro = acervo.Pesquisar(isbn);
                if (livro != null)
                {
                    Console.Write("Tombo: "); int tombo = int.Parse(Console.ReadLine());
                    var exemplar = livro.Exemplares.FirstOrDefault(e => e.Tombo == tombo);
                    if (exemplar != null && exemplar.Emprestar())
                        Console.WriteLine("Empréstimo registrado com sucesso.");
                    else
                        Console.WriteLine("Exemplar não disponível para empréstimo.");
                }
            }
            else if (opcao == 6)
            {
                Console.Write("ISBN: "); int isbn = int.Parse(Console.ReadLine());
                var livro = acervo.Pesquisar(isbn);
                if (livro != null)
                {
                    Console.Write("Tombo: "); int tombo = int.Parse(Console.ReadLine());
                    var exemplar = livro.Exemplares.FirstOrDefault(e => e.Tombo == tombo);
                    if (exemplar != null && exemplar.Devolver())
                        Console.WriteLine("Devolução registrada com sucesso.");
                    else
                        Console.WriteLine("Erro ao registrar devolução.");
                }
            }
        }
    }
}
