using EscolaDeCursos.WebApp.Modulos.ModuloCurso.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloTurma.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma.Apresentacao;

public class TurmaController(
    IRepositorioTurma repositorioTurma,
    IRepositorioCurso repositorioCurso,
    IRepositorioInstrutor repositorioInstrutor,
    IRepositorioMatricula repositorioMatricula
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarTurmaViewModel> listarVms = repositorioTurma
            .SelecionarTodos()
            .Select(t => new ListarTurmaViewModel(
                t.Id,
                t.Nome,
                t.Curso.Nome,
                t.Instrutor.Nome,
                t.NumeroMaximoAlunos,
                t.DataInicio,
                t.DataTermino,
                t.Matriculas.Count
            ))
            .ToList();

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarTurmaViewModel cadastrarVm = new(
            string.Empty,
            null,
            null,
            null,
            null,
            null
        );

        CarregarCursosEInstrutores();

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarTurmaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarCursosEInstrutores();
            return View(cadastrarVm);
        }

        Curso? curso = repositorioCurso.SelecionarPorId(cadastrarVm.CursoId!.Value);
        if (curso == null)
            ModelState.AddModelError(nameof(cadastrarVm.CursoId), "Selecione um curso válido.");

        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(cadastrarVm.InstrutorId!.Value);
        if (instrutor == null)
            ModelState.AddModelError(nameof(cadastrarVm.InstrutorId), "Selecione um instrutor válido.");

        if (!ModelState.IsValid)
        {
            CarregarCursosEInstrutores();
            return View(cadastrarVm);
        }

        Turma novaTurma = new(
            cadastrarVm.Nome,
            curso!,
            instrutor!,
            cadastrarVm.NumeroMaximoAlunos!.Value,
            cadastrarVm.DataInicio!.Value,
            cadastrarVm.DataTermino!.Value
        );

        string? erroValidacao = novaTurma.Validar().FirstOrDefault();
        if (erroValidacao != null)
            ModelState.AddModelError(string.Empty, erroValidacao);

        if (!ModelState.IsValid)
        {
            CarregarCursosEInstrutores();
            return View(cadastrarVm);
        }

        repositorioTurma.Cadastrar(novaTurma);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Turma? turma = repositorioTurma.SelecionarPorId(id);

        if (turma == null)
        {
            return RedirectToAction(nameof(Listar));
        }

        EditarTurmaViewModel editarVm = new(
            turma.Id,
            turma.Nome,
            turma.Curso.Id,
            turma.Instrutor.Id,
            turma.NumeroMaximoAlunos,
            turma.DataInicio,
            turma.DataTermino
        );

        CarregarCursosEInstrutores();

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarTurmaViewModel editarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarCursosEInstrutores();
            return View(editarVm);
        }

        Curso? curso = repositorioCurso.SelecionarPorId(editarVm.CursoId!.Value);
        if (curso == null)
            ModelState.AddModelError(nameof(editarVm.CursoId), "Selecione um curso válido.");

        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(editarVm.InstrutorId!.Value);
        if (instrutor == null)
            ModelState.AddModelError(nameof(editarVm.InstrutorId), "Selecione um instrutor válido.");

        if (!ModelState.IsValid)
        {
            CarregarCursosEInstrutores();
            return View(editarVm);
        }

        Turma turmaAtualizada = new(
            editarVm.Nome,
            curso!,
            instrutor!,
            editarVm.NumeroMaximoAlunos!.Value,
            editarVm.DataInicio!.Value,
            editarVm.DataTermino!.Value
        );

        string? erroValidacao = turmaAtualizada.Validar().FirstOrDefault();
        if (erroValidacao != null)
            ModelState.AddModelError(string.Empty, erroValidacao);

        int quantidadeMatriculas = repositorioMatricula.ContarPorTurmaId(editarVm.Id);
        if (turmaAtualizada.NumeroMaximoAlunos < quantidadeMatriculas)
        {
            ModelState.AddModelError(
                nameof(editarVm.NumeroMaximoAlunos),
                "O número máximo de alunos não pode ser menor que a quantidade de matrículas atuais."
            );
        }

        if (!ModelState.IsValid)
        {
            CarregarCursosEInstrutores();
            return View(editarVm);
        }

        if (!repositorioTurma.Editar(editarVm.Id, turmaAtualizada))
        {
            ModelState.AddModelError(string.Empty, "Turma não encontrada.");
            CarregarCursosEInstrutores();
            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Turma? turma = repositorioTurma.SelecionarPorId(id);

        if (turma == null)
        {
            return RedirectToAction(nameof(Listar));
        }

        ExcluirTurmaViewModel excluirVm = new(
            turma.Id,
            turma.Nome,
            turma.Curso.Nome,
            turma.Instrutor.Nome,
            turma.NumeroMaximoAlunos,
            turma.DataInicio,
            turma.DataTermino
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirTurmaViewModel excluirVm)
    {
        Turma? turma = repositorioTurma.SelecionarPorId(excluirVm.Id);

        if (turma != null && !repositorioMatricula.ExistePorTurmaId(excluirVm.Id))
            repositorioTurma.Excluir(excluirVm.Id);

        return RedirectToAction(nameof(Listar));
    }

    private void CarregarCursosEInstrutores()
    {
        ViewBag.Cursos = repositorioCurso
            .SelecionarTodos()
            .Select(c => new SelectListItem(c.Nome, c.Id.ToString()))
            .ToList();

        ViewBag.Instrutores = repositorioInstrutor
            .SelecionarTodos()
            .Select(i => new SelectListItem(i.Nome, i.Id.ToString()))
            .ToList();
    }
}
