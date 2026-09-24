using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Orm;
using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloAluno.Infraestrutura;

public sealed class RepositorioAlunoEmOrm : IRepositorioAluno
{
    private readonly EscolaDeCursosDbContext dbContext;

    public RepositorioAlunoEmOrm(EscolaDeCursosDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Cadastrar(Aluno entidade)
    {
        dbContext.Alunos.Add(entidade); //Adiciona em memória

        dbContext.SaveChanges(); //Salva em banco
    }

    public bool Editar(Guid idSelecionado, Aluno entidadeAtualizada)
    {
        Aluno? alunoSelecionado = SelecionarPorId(idSelecionado);

        if (alunoSelecionado == null)
            return false;

        alunoSelecionado.Atualizar(entidadeAtualizada);

        dbContext.SaveChanges();

        return true;
    }

    public bool Excluir(Guid idSelecionado)
    {
        Aluno? alunoSelecionado = SelecionarPorId(idSelecionado);

        if (alunoSelecionado == null)
            return false;

        dbContext.Alunos.Remove(alunoSelecionado);

        dbContext.SaveChanges();

        return true;
    }

    public Aluno? SelecionarPorId(Guid idSelecionado)
    {
        return dbContext.Alunos.SingleOrDefault(a => a.Id == idSelecionado);
    }

    public List<Aluno> SelecionarTodos()
    {
        return dbContext.Alunos.ToList();
    }
}
