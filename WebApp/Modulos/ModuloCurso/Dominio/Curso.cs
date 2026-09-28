using EscolaDeCursos.WebApp.Compartilhado.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.Dominio;

public class Curso : EntidadeBase<Curso>
{
    public string Nome { get; set; } = string.Empty;
    public NivelCurso Nivel { get; set; }
    public int CargaHoraria { get; set; }
    public List<Aula> Aulas { get; set; } = [];

    public Curso()
    {
    }

    public Curso(
        string nome,
        NivelCurso nivel,
        int cargaHoraria
    ) : this()
    {
        Nome = nome;
        Nivel = nivel;
        CargaHoraria = cargaHoraria;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (!Enum.IsDefined(Nivel))
            erros.Add("O campo \"Nível\" deve ser preenchido.");

        if (CargaHoraria < 2 || CargaHoraria > 100)
            erros.Add("O campo \"Carga Horária\" deve estar entre 2 e 100 horas.");

        return erros;
    }

    public override void Atualizar(Curso entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Nivel = entidadeAtualizada.Nivel;
        CargaHoraria = entidadeAtualizada.CargaHoraria;
    }
}
