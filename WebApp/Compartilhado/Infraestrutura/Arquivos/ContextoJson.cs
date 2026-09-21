using System.Text.Json;
using System.Text.Json.Serialization;
using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Dominio;

namespace EscolaDeCursos.WebApp.Compartilhado.Infraestrutura.Arquivos;

public sealed class ContextoJson
{
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        WriteIndented = true,
        ReferenceHandler = ReferenceHandler.Preserve
    };

    private readonly string caminhoArquivoDados;

    public List<Instrutor> Instrutores { get; set; } = [];
    public List<Aluno> Alunos { get; set; } = [];

    public ContextoJson()
    {
        string caminhoAppData = Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData
        );

        string caminhoDiretorio = Path.Join(caminhoAppData, "EscolaDeCursos-Backend");

        caminhoArquivoDados = Path.Join(caminhoDiretorio, "dados.json");
    }

    public ContextoJson(string caminhoArquivoDados)
    {
        this.caminhoArquivoDados = caminhoArquivoDados;
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivoDados))
            return;

        string json = File.ReadAllText(caminhoArquivoDados);

        if (string.IsNullOrWhiteSpace(json))
            return;

        ContextoJson? contextoSalvo = JsonSerializer.Deserialize<ContextoJson>(json, jsonOptions);

        if (contextoSalvo == null)
            return;

        SubstituirConteudo(Instrutores, contextoSalvo.Instrutores);
        SubstituirConteudo(Alunos, contextoSalvo.Alunos);
    }

    public void Salvar()
    {
        string? caminhoDiretorio = Path.GetDirectoryName(caminhoArquivoDados);

        if (!string.IsNullOrWhiteSpace(caminhoDiretorio))
            Directory.CreateDirectory(caminhoDiretorio);

        string json = JsonSerializer.Serialize(this, jsonOptions);

        File.WriteAllText(caminhoArquivoDados, json);
    }

    private static void SubstituirConteudo<T>(List<T> destino, List<T> origem)
    {
        destino.Clear();
        destino.AddRange(origem);
    }

}
