using EscolaDeCursos.WebApp.Compartilhado.Apresentacao;
using EscolaDeCursos.WebApp.Compartilhado.Infraestrutura;

var builder = WebApplication.CreateBuilder(args);

// Configuração do container de injeção de dependência
builder.Services.AddInfraRepositories(builder.Configuration);
builder.Services.AddPresentationConfig();

var app = builder.Build();

// Middlewares de roteamento
app.UseRouting();
app.MapDefaultControllerRoute();

// Execução do Servidor
app.Run();
