using EscolaDeCursos.WebApp.Compartilhado.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.Dominio;

public interface IRepositorioAula : IRepositorio<Aula>
{
    bool ExisteComNome(Guid cursoId, string nome, Guid? idIgnorado = null);
    bool ExisteComOrdem(Guid cursoId, int ordem, Guid? idIgnorado = null);
    bool ExistePorCursoId(Guid cursoId);
    List<Aula> SelecionarPorCursoId(Guid cursoId);
}
