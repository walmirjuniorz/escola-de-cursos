using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Orm;
using EscolaDeCursos.WebApp.Modulos.ModuloCurso.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.Infraestrutura;

public sealed class RepositorioAulaEmOrm : IRepositorioAula
{
    private readonly EscolaDeCursosDbContext dbContext;

    public RepositorioAulaEmOrm(EscolaDeCursosDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Cadastrar(Aula entidade)
    {
        dbContext.Aulas.Add(entidade);

        dbContext.SaveChanges();
    }

    public bool Editar(Guid idSelecionado, Aula entidadeAtualizada)
    {
        Aula? aulaSelecionada = SelecionarPorId(idSelecionado);

        if (aulaSelecionada == null)
            return false;

        aulaSelecionada.Atualizar(entidadeAtualizada);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Guid idSelecionado)
    {
        Aula? aulaSelecionada = SelecionarPorId(idSelecionado);

        if (aulaSelecionada == null)
            return false;

        dbContext.Aulas.Remove(aulaSelecionada);

        dbContext.SaveChanges();

        return true;
    }

    public Aula? SelecionarPorId(Guid idSelecionado)
    {
        return dbContext.Aulas.SingleOrDefault(c => c.Id == idSelecionado);
    }

    public List<Aula> SelecionarTodos()
    {
        return dbContext.Aulas.OrderByDescending(c => c.Id).ToList();
    }

    public List<Aula> SelecionarPorCursoId(Guid cursoId)
    {
        return dbContext.Aulas
            .Where(a => a.CursoId == cursoId)
            .OrderBy(a => a.Ordem)
            .ToList();
    }

    public bool ExisteComNome(Guid cursoId, string nome, Guid? idIgnorado = null)
    {
        return dbContext.Aulas.Any(a =>
            a.Id != idIgnorado &&
            a.CursoId == cursoId &&
            a.Nome.ToLower() == nome.ToLower()
        );
    }

    public bool ExisteComOrdem(Guid cursoId, int ordem, Guid? idIgnorado = null)
    {
        return dbContext.Aulas.Any(a =>
            a.Id != idIgnorado &&
            a.CursoId == cursoId &&
            a.Ordem == ordem
        );
    }

    public bool ExistePorCursoId(Guid cursoId)
    {
        return dbContext.Aulas
            .Any(a => a.CursoId == cursoId);
    }
}
