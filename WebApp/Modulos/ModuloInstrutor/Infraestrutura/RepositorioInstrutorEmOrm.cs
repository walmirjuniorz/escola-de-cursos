using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Orm;
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Infraestrutura;

// ORM = Object Relational Mapping
public sealed class RepositorioInstrutorEmOrm : IRepositorioInstrutor
{

    private readonly EscolaDeCursosDbContext dbContext;

    public RepositorioInstrutorEmOrm(EscolaDeCursosDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Cadastrar(Instrutor entidade)
    {
        dbContext.Instrutores.Add(entidade);

        dbContext.SaveChanges();
    }

    public bool Editar(Guid idSelecionado, Instrutor entidadeAtualizada)
    {
        Instrutor? instrutor = SelecionarPorId(idSelecionado);

        if (instrutor == null)
            return false;

        instrutor.Atualizar(entidadeAtualizada);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Guid idSelecionado)
    {
        Instrutor? instrutor = SelecionarPorId(idSelecionado);

        if (instrutor == null)
            return false;

        dbContext.Instrutores.Remove(instrutor);

        dbContext.SaveChanges();

        return true;
    }

    public Instrutor? SelecionarPorId(Guid idSelecionado)
    {
        return dbContext.Instrutores.SingleOrDefault(i => i.Id == idSelecionado);
    }

    public List<Instrutor> SelecionarTodos()
    {
        return dbContext.Instrutores.ToList();
    }
    public bool ExisteComNome(string nome, Guid? idIgnorado = null)
    {
        return dbContext.Instrutores.Any(i =>
            i.Id != idIgnorado &&
            i.Nome.Trim() == nome.Trim()
        );
    }
}
