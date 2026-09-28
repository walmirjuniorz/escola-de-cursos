using EscolaDeCursos.WebApp.Compartilhado.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.Dominio;

public interface IRepositorioCurso : IRepositorio<Curso>
{
    bool ExisteComNome(string nome, Guid? idIgnorado = null);
}
