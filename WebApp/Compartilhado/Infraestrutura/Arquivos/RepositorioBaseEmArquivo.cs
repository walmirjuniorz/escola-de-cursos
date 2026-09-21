using EscolaDeCursos.WebApp.Compartilhado.Dominio;

namespace EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Arquivos;

public abstract class RepositorioBaseEmArquivo<TEntidade>
    where TEntidade : EntidadeBase<TEntidade>
{
    protected readonly ContextoJson contexto;
    protected readonly List<TEntidade> registros;

    protected RepositorioBaseEmArquivo(ContextoJson contexto)
    {
        this.contexto = contexto;
        registros = ObterRegistros(contexto);
    }

    public virtual void Cadastrar(TEntidade entidade)
    {
        if (entidade.Id == Guid.Empty)
            entidade.Id = Guid.CreateVersion7();

        registros.Add(entidade);

        contexto.Salvar();
    }

    public virtual bool Editar(Guid idSelecionado, TEntidade entidadeAtualizada)
    {
        TEntidade? entidadeSelecionada = SelecionarPorId(idSelecionado);

        if (entidadeSelecionada == null)
            return false;

        entidadeSelecionada.Atualizar(entidadeAtualizada);

        contexto.Salvar();

        return true;
    }

    public virtual bool Excluir(Guid idSelecionado)
    {
        TEntidade? entidadeSelecionada = SelecionarPorId(idSelecionado);

        if (entidadeSelecionada == null || !registros.Remove(entidadeSelecionada))
            return false;

        contexto.Salvar();

        return true;
    }

    public virtual TEntidade? SelecionarPorId(Guid idSelecionado)
    {
        return registros.SingleOrDefault(r => r.Id == idSelecionado);
    }

    public virtual List<TEntidade> SelecionarTodos()
    {
        return registros.ToList();
    }

    protected abstract List<TEntidade> ObterRegistros(ContextoJson contexto);
}
