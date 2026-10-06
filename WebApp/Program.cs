using EscolaDeCursos.WebApp.Compartilhado.Apresentacao;
using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura;
using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Orm;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string dataProtectionKeysPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    "EscolaDeCursos",
    "DataProtection-Keys"
);

Directory.CreateDirectory(dataProtectionKeysPath);

IDataProtectionBuilder dataProtection = builder.Services
    .AddDataProtection()
    .SetApplicationName("EscolaDeCursos")
    .PersistKeysToFileSystem(new DirectoryInfo(dataProtectionKeysPath));

// Configuração do container de injeção de dependência
builder.Services.AddInfraRepositories(builder.Configuration);
builder.Services.AddPresentationConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<EscolaDeCursosDbContext>();

    if (dbContext.Database.IsSqlServer())
        dbContext.Database.Migrate();
}

// Middlewares de roteamento
app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();
