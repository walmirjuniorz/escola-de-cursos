using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Arquivos;
using EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Infraestrutura;

public sealed class RepositorioMatriculaEmArquivo(ContextoJson contexto)
    : RepositorioBaseEmArquivo<Matricula>(contexto), IRepositorioMatricula
{
    public override void Cadastrar(Matricula entidade)
    {
        entidade.Turma.Matriculas.Add(entidade);

        base.Cadastrar(entidade);
    }

    public override bool Editar(Guid idSelecionado, Matricula entidadeAtualizada)
    {
        Matricula? matricula = SelecionarPorId(idSelecionado);

        if (matricula == null)
            return false;

        matricula.Turma.Matriculas.Remove(matricula);
        matricula.Atualizar(entidadeAtualizada);
        matricula.Turma.Matriculas.Add(matricula);

        contexto.Salvar();

        return true;
    }

    public override bool Excluir(Guid idSelecionado)
    {
        Matricula? matricula = SelecionarPorId(idSelecionado);

        if (matricula == null)
            return false;

        matricula.Turma.Matriculas.Remove(matricula);

        return base.Excluir(idSelecionado);
    }

    public bool Existe(Guid turmaId, Guid alunoId, Guid? idIgnorado = null)
    {
        return registros.Any(m =>
            m.Turma.Id == turmaId &&
            m.Aluno.Id == alunoId &&
            m.Id != idIgnorado
        );
    }

    public bool ExistePorAlunoId(Guid alunoId)
    {
        return registros.Any(m => m.Aluno.Id == alunoId);
    }

    public bool ExistePorTurmaId(Guid turmaId)
    {
        return registros.Any(m => m.Turma.Id == turmaId);
    }

    public int ContarPorTurmaId(Guid turmaId)
    {
        return registros.Count(m => m.Turma.Id == turmaId);
    }

    public List<Matricula> SelecionarPorTurmaId(Guid turmaId)
    {
        return registros
            .Where(m => m.Turma.Id == turmaId)
            .OrderBy(m => m.Aluno.Nome)
            .ToList();
    }

    public override List<Matricula> SelecionarTodos()
    {
        return registros.OrderBy(m => m.Aluno.Nome).ToList();
    }

    protected override List<Matricula> ObterRegistros(ContextoJson contexto)
    {
        return contexto.Matriculas;
    }
}
