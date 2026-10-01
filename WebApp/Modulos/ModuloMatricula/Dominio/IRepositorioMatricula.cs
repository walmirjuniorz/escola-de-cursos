using EscolaDeCursos.WebApp.Compartilhado.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Dominio;

public interface IRepositorioMatricula : IRepositorio<Matricula>
{
    bool Existe(Guid turmaId, Guid alunoId, Guid? idIgnorado = null);
    bool ExistePorAlunoId(Guid alunoId);
    bool ExistePorTurmaId(Guid turmaId);
    int ContarPorTurmaId(Guid turmaId);
    List<Matricula> SelecionarPorTurmaId(Guid turmaId);
}
