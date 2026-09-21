using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Arquivos;
using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloAluno.Infraestrutura;

public sealed class RepositorioAlunoEmArquivo(ContextoJson contexto)
    : RepositorioBaseEmArquivo<Aluno>(contexto), IRepositorioAluno
{
    protected override List<Aluno> ObterRegistros(ContextoJson contexto)
    {
        return contexto.Alunos;
    }
}
