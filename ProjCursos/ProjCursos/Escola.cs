using System;
using System.Collections.Generic;
using System.Linq;

public class Escola
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Disciplina> Disciplinas { get; private set; }

        public Aluno(int id, string nome)
        {
            Id = id;
            Nome = nome;
            Disciplinas = new List<Disciplina>();
        }

        public bool PodeMatricular()
        {
            return Disciplinas.Count < 6;
        }
    }

    public class Disciplina
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<Aluno> Alunos { get; private set; }

        public Disciplina(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
            Alunos = new List<Aluno>();
        }

        public bool MatricularAluno(Aluno aluno)
        {
            if (Alunos.Count < 15 && aluno.PodeMatricular())
            {
                Alunos.Add(aluno);
                aluno.Disciplinas.Add(this);
                return true;
            }
            return false;
        }

        public bool DesmatricularAluno(Aluno aluno)
        {
            if (Alunos.Remove(aluno))
            {
                aluno.Disciplinas.Remove(this);
                return true;
            }
            return false;
        }
    }

    public class Curso
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<Disciplina> Disciplinas { get; private set; }

        public Curso(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
            Disciplinas = new List<Disciplina>();
        }

        public bool AdicionarDisciplina(Disciplina disciplina)
        {
            if (Disciplinas.Count < 12)
            {
                Disciplinas.Add(disciplina);
                return true;
            }
            return false;
        }

        public Disciplina PesquisarDisciplina(int disciplinaId)
        {
            return Disciplinas.FirstOrDefault(d => d.Id == disciplinaId);
        }

        public bool RemoverDisciplina(Disciplina disciplina)
        {
            if (disciplina.Alunos.Count == 0)
            {
                return Disciplinas.Remove(disciplina);
            }
            return false;
        }
    }

    public List<Curso> Cursos { get; private set; }

    public Escola()
    {
        Cursos = new List<Curso>();
    }

    public bool AdicionarCurso(Curso curso)
    {
        if (Cursos.Count < 5)
        {
            Cursos.Add(curso);
            return true;
        }
        return false;
    }

    public Curso PesquisarCurso(int cursoId)
    {
        return Cursos.FirstOrDefault(c => c.Id == cursoId);
    }

    public bool RemoverCurso(Curso curso)
    {
        if (curso.Disciplinas.Count == 0)
        {
            return Cursos.Remove(curso);
        }
        return false;
    }
}
