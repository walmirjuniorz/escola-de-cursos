using EscolaDeCursos.WebApp.Compartilhado.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloTurma.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Dominio;

public class Matricula : EntidadeBase<Matricula>
{
    public Guid AlunoId { get; set; } = Guid.Empty;
    public Aluno Aluno { get; set; } = null!;
    public Guid TurmaId { get; set; } = Guid.Empty;
    public Turma Turma { get; set; } = null!;

    public Matricula()
    {
    }

    public Matricula(Aluno aluno, Turma turma) : this()
    {
        Aluno = aluno;
        Turma = turma;
    }

    public override List<string> Validar()
    {
        return [];
    }

    public override void Atualizar(Matricula entidadeAtualizada)
    {
        Aluno = entidadeAtualizada.Aluno;
        Turma = entidadeAtualizada.Turma;
    }
}
