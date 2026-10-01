using EscolaDeCursos.WebApp.Compartilhado.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloCurso.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Dominio;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma.Dominio;

public class Turma : EntidadeBase<Turma>
{
    public string Nome { get; set; } = string.Empty;
    public Guid CursoId { get; set; } = Guid.Empty;
    public Curso Curso { get; set; } = null!;
    public Guid InstrutorId { get; set; } = Guid.Empty;
    public Instrutor Instrutor { get; set; } = null!;
    public int NumeroMaximoAlunos { get; set; }
    public DateOnly DataInicio { get; set; }
    public DateOnly DataTermino { get; set; }
    public List<Matricula> Matriculas { get; set; } = [];

    public Turma()
    {
    }

    public Turma(
        string nome,
        Curso curso,
        Instrutor instrutor,
        int numeroMaximoAlunos,
        DateOnly dataInicio,
        DateOnly dataTermino
    ) : this()
    {
        Nome = nome;
        Curso = curso;
        Instrutor = instrutor;
        NumeroMaximoAlunos = numeroMaximoAlunos;
        DataInicio = dataInicio;
        DataTermino = dataTermino;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        if (NumeroMaximoAlunos < 1 || NumeroMaximoAlunos > 100)
            erros.Add("O campo \"Número Máximo de Alunos\" deve estar entre 1 e 100.");

        if (DataInicio == default)
            erros.Add("O campo \"Data de Início\" deve ser preenchido.");

        if (DataTermino == default)
            erros.Add("O campo \"Data de Término\" deve ser preenchido.");

        if (DataTermino <= DataInicio)
            erros.Add("A data de término deve ser posterior à data de início.");

        return erros;
    }

    public override void Atualizar(Turma entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Curso = entidadeAtualizada.Curso;
        Instrutor = entidadeAtualizada.Instrutor;
        NumeroMaximoAlunos = entidadeAtualizada.NumeroMaximoAlunos;
        DataInicio = entidadeAtualizada.DataInicio;
        DataTermino = entidadeAtualizada.DataTermino;
    }
}
