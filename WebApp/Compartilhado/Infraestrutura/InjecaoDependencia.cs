using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Orm;
using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Infraestrutura;
using EscolaDeCursos.WebApp.Modulos.ModuloCurso.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloCurso.Infraestrutura;
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Infraestrutura;
using EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Infraestrutura;
using EscolaDeCursos.WebApp.Modulos.ModuloTurma.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloTurma.Infraestrutura;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.WebApp.Compartilhado.Infraestrutura;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        string? connectionString = configuration.GetConnectionString("SqlServer");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "A connection string 'SqlServer' não foi encontrada."
            );
        }

        services.AddDbContext<EscolaDeCursosDbContext>(options =>
            options.UseSqlServer(connectionString, config =>
            {
                config.EnableRetryOnFailure(3); // Brasil <-> Canadá = Latência 300ms
            })
        );

        services.AddScoped<IRepositorioInstrutor, RepositorioInstrutorEmOrm>();
        services.AddScoped<IRepositorioAluno, RepositorioAlunoEmOrm>();
        services.AddScoped<IRepositorioCurso, RepositorioCursoEmOrm>();
        services.AddScoped<IRepositorioAula, RepositorioAulaEmOrm>();
        services.AddScoped<IRepositorioTurma, RepositorioTurmaEmOrm>();
        services.AddScoped<IRepositorioMatricula, RepositorioMatriculaEmOrm>();
    }
}
