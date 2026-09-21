using EscolaDeCursos.WebApp.Compartilhado.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Dominio;

public interface IRepositorioInstrutor : IRepositorio<Instrutor>
{
    bool ExisteComNome(string nome, Guid? idIgnorado = null);
}
