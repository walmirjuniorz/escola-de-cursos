using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Arquivos;
using EscolaDeCursos.WebApp.Modulos.ModuloTurma.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma.Infraestrutura;

public sealed class RepositorioTurmaEmArquivo(ContextoJson contexto)
    : RepositorioBaseEmArquivo<Turma>(contexto), IRepositorioTurma
{
    public bool ExistePorCursoId(Guid cursoId)
    {
        return registros.Any(t => t.Curso.Id == cursoId);
    }

    public bool ExistePorInstrutorId(Guid instrutorId)
    {
        return registros.Any(t => t.Instrutor.Id == instrutorId);
    }

    public override List<Turma> SelecionarTodos()
    {
        return registros
            .OrderBy(t => t.DataInicio)
            .ThenBy(t => t.Nome)
            .ToList();
    }

    public override bool Excluir(Guid idSelecionado)
    {
        bool possuiMatriculas = contexto.Matriculas.Any(m => m.Turma.Id == idSelecionado);

        return !possuiMatriculas && base.Excluir(idSelecionado);
    }

    protected override List<Turma> ObterRegistros(ContextoJson contexto)
    {
        return contexto.Turmas;
    }
}
