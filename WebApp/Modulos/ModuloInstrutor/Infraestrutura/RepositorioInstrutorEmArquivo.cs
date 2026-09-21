using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Arquivos;
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Infraestrutura;

public sealed class RepositorioInstrutorEmArquivo(ContextoJson contexto)
    : RepositorioBaseEmArquivo<Instrutor>(contexto), IRepositorioInstrutor
{
    public bool ExisteComNome(string nome, Guid? idIgnorado = null)
    {
        return registros.Any(i =>
            i.Id != idIgnorado &&
            string.Equals(i.Nome.Trim(), nome.Trim(), StringComparison.OrdinalIgnoreCase)
        );
    }

    protected override List<Instrutor> ObterRegistros(ContextoJson contexto)
    {
        return contexto.Instrutores;
    }
}
