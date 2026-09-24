using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Infraestrutura;
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Infraestrutura;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Orm;

public sealed class EscolaDeCursosDbContext : DbContext
{
    public DbSet<Instrutor> Instrutores => Set<Instrutor>();
    public DbSet<Aluno> Alunos => Set<Aluno>();

    public EscolaDeCursosDbContext(DbContextOptions<EscolaDeCursosDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new InstrutorConfiguration());
        modelBuilder.ApplyConfiguration(new AlunoConfiguration());
    }
}
