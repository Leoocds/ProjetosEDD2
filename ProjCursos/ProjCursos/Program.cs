class Program
{
    static void Main(string[] args)
    {
        Escola escola = new Escola();

        while (true)
        {
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar curso");
            Console.WriteLine("2. Pesquisar curso");
            Console.WriteLine("3. Remover curso");
            Console.WriteLine("4. Adicionar disciplina no curso");
            Console.WriteLine("5. Pesquisar disciplina");
            Console.WriteLine("6. Remover disciplina do curso");
            Console.WriteLine("7. Matricular aluno na disciplina");
            Console.WriteLine("8. Remover aluno da disciplina");
            Console.WriteLine("9. Pesquisar aluno");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Saindo...");
                    return;

                case 1:
                    Console.Write("ID do curso: ");
                    int idCurso = int.Parse(Console.ReadLine());
                    Console.Write("Descrição do curso: ");
                    string descCurso = Console.ReadLine();
                    var curso = new Escola.Curso(idCurso, descCurso);

                    if (escola.AdicionarCurso(curso))
                        Console.WriteLine("Curso adicionado com sucesso.");
                    else
                        Console.WriteLine("Não é possível adicionar mais cursos.");
                    break;

                case 2:
                    Console.Write("ID do curso: ");
                    int cursoId = int.Parse(Console.ReadLine());
                    var cursoPesquisado = escola.PesquisarCurso(cursoId);

                    if (cursoPesquisado != null)
                    {
                        Console.WriteLine($"Curso: {cursoPesquisado.Descricao}");
                        foreach (var disciplina in cursoPesquisado.Disciplinas)
                        {
                            Console.WriteLine($"  Disciplina: {disciplina.Descricao}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Curso não encontrado.");
                    }
                    break;

                case 4:
                    Console.Write("ID do curso: ");
                    int idCursoDisc = int.Parse(Console.ReadLine());
                    var cursoAddDisc = escola.PesquisarCurso(idCursoDisc);

                    if (cursoAddDisc != null)
                    {
                        Console.Write("ID da disciplina: ");
                        int idDisc = int.Parse(Console.ReadLine());
                        Console.Write("Descrição da disciplina: ");
                        string descDisc = Console.ReadLine();

                        var disciplina = new Escola.Disciplina(idDisc, descDisc);

                        if (cursoAddDisc.AdicionarDisciplina(disciplina))
                            Console.WriteLine("Disciplina adicionada com sucesso.");
                        else
                            Console.WriteLine("Curso já possui o número máximo de disciplinas.");
                    }
                    else
                    {
                        Console.WriteLine("Curso não encontrado.");
                    }
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
