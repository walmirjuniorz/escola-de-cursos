using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Arquivos;
using EscolaDeCursos.WebApp.Modulos.ModuloCurso.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.Infraestrutura;

public sealed class RepositorioAulaEmArquivo(ContextoJson contexto)
    : RepositorioBaseEmArquivo<Aula>(contexto), IRepositorioAula
{
    public override void Cadastrar(Aula entidade)
    {
        entidade.Curso.Aulas.Add(entidade);

        base.Cadastrar(entidade);
    }

    public override bool Excluir(Guid idSelecionado)
    {
        Aula? aula = SelecionarPorId(idSelecionado);

        if (aula == null)
            return false;

        aula.Curso.Aulas.Remove(aula);

        return base.Excluir(idSelecionado);
    }

    public bool ExisteComNome(Guid cursoId, string nome, Guid? idIgnorado = null)
    {
        return registros.Any(a =>
            a.Id != idIgnorado &&
            a.Curso.Id == cursoId &&
            a.Nome.Trim() == nome.Trim()
        );
    }

    public bool ExisteComOrdem(Guid cursoId, int ordem, Guid? idIgnorado = null)
    {
        return registros.Any(a =>
            a.Curso.Id == cursoId &&
            a.Ordem == ordem &&
            a.Id != idIgnorado
        );
    }

    public bool ExistePorCursoId(Guid cursoId)
    {
        return registros.Any(a => a.Curso.Id == cursoId);
    }

    public List<Aula> SelecionarPorCursoId(Guid cursoId)
    {
        return registros
            .Where(a => a.Curso.Id == cursoId)
            .OrderBy(a => a.Ordem)
            .ToList();
    }

    public override List<Aula> SelecionarTodos()
    {
        return registros
            .OrderBy(a => a.Curso.Id)
            .ThenBy(a => a.Ordem)
            .ToList();
    }

    protected override List<Aula> ObterRegistros(ContextoJson contexto)
    {
        return contexto.Aulas;
    }
}
