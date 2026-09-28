using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Orm;
using EscolaDeCursos.WebApp.Modulos.ModuloCurso.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.Infraestrutura;

public sealed class RepositorioCursoEmOrm : IRepositorioCurso
{
    private readonly EscolaDeCursosDbContext dbContext;

    public RepositorioCursoEmOrm(EscolaDeCursosDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Cadastrar(Curso entidade)
    {
        dbContext.Cursos.Add(entidade);

        dbContext.SaveChanges();
    }

    public bool Editar(Guid idSelecionado, Curso entidadeAtualizada)
    {
        Curso? cursoSelecionado = SelecionarPorId(idSelecionado);

        if (cursoSelecionado == null)
            return false;

        cursoSelecionado.Atualizar(entidadeAtualizada);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Guid idSelecionado)
    {
        Curso? cursoSelecionado = SelecionarPorId(idSelecionado);

        if (cursoSelecionado == null)
            return false;

        dbContext.Cursos.Remove(cursoSelecionado);

        dbContext.SaveChanges();

        return true;
    }

    public Curso? SelecionarPorId(Guid idSelecionado)
    {
        return dbContext.Cursos
            .Include(c => c.Aulas)
            .SingleOrDefault(c => c.Id == idSelecionado);
    }

    public List<Curso> SelecionarTodos()
    {
        return dbContext.Cursos.OrderByDescending(c => c.Id).ToList();
    }

    public bool ExisteComNome(string nome, Guid? idIgnorado = null)
    {
        return dbContext.Cursos.Any(c =>
            c.Id != idIgnorado &&
            c.Nome.ToLower() == nome.ToLower()
        );
    }
}
