using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Orm;
using EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Infraestrutura;

public sealed class RepositorioMatriculaEmOrm : IRepositorioMatricula
{
    private readonly EscolaDeCursosDbContext dbContext;

    public RepositorioMatriculaEmOrm(EscolaDeCursosDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Cadastrar(Matricula entidade)
    {
        dbContext.Matriculas.Add(entidade);

        dbContext.SaveChanges();
    }

    public bool Editar(Guid idSelecionado, Matricula entidadeAtualizada)
    {
        Matricula? matriculaSelecionada = SelecionarPorId(idSelecionado);

        if (matriculaSelecionada == null)
            return false;

        matriculaSelecionada.Atualizar(entidadeAtualizada);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Guid idSelecionado)
    {
        Matricula? matriculaSelecionada = SelecionarPorId(idSelecionado);

        if (matriculaSelecionada == null)
            return false;

        dbContext.Matriculas.Remove(matriculaSelecionada);

        dbContext.SaveChanges();

        return true;
    }

    public Matricula? SelecionarPorId(Guid idSelecionado)
    {
        return dbContext.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
            .SingleOrDefault(m => m.Id == idSelecionado);
    }

    public List<Matricula> SelecionarTodos()
    {
        return dbContext.Matriculas
            .Include(m => m.Aluno)
            .Include(m => m.Turma)
            .ToList();
    }

    public List<Matricula> SelecionarPorTurmaId(Guid turmaId)
    {
        return dbContext.Matriculas
            .Where(m => m.TurmaId == turmaId)
            .ToList();
    }

    public int ContarPorTurmaId(Guid turmaId)
    {
        return dbContext.Matriculas
            .Count(m => m.TurmaId == turmaId);
    }

    public bool Existe(Guid turmaId, Guid alunoId, Guid? idIgnorado = null)
    {
        return dbContext.Matriculas.Any(m =>
            m.TurmaId == turmaId &&
            m.AlunoId == alunoId &&
            m.Id != idIgnorado
        );
    }

    public bool ExistePorAlunoId(Guid alunoId)
    {
        return dbContext.Matriculas.Any(m => m.AlunoId == alunoId);
    }

    public bool ExistePorTurmaId(Guid turmaId)
    {
        return dbContext.Matriculas.Any(m => m.TurmaId == turmaId);
    }
}
