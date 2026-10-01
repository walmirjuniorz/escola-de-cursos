using EscolaDeCursos.WebApp.Compartilhado.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma.Dominio;

public interface IRepositorioTurma : IRepositorio<Turma>
{
    bool ExistePorCursoId(Guid cursoId);
    bool ExistePorInstrutorId(Guid instrutorId);
}
