using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Dominio;
using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Arquivos;
using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Infraestrutura;
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Infraestrutura;
using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Orm;
using Microsoft.EntityFrameworkCore;

namespace EscolaDeCursos.WebApp.Compartilhado.Infraestrutura;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ContextoJson>(_ =>
        {
            //Configura persistencia em arquivo
            ContextoJson contexto = new();
            contexto.Carregar();
            return contexto;
        });

        // Configura persistencia em banco de dados
        services.AddDbContext<EscolaDeCursosDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("SqlServerDocker");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException($"A Connection String \"SqlServerDocker\" não foi encontrada!");
            }

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IRepositorioInstrutor, RepositorioInstrutorEmOrm>();
        services.AddScoped<IRepositorioAluno, RepositorioAlunoEmArquivo>();
    }
}
