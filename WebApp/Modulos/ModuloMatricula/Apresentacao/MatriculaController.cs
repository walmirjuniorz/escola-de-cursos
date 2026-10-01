using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Dominio;
using EscolaDeCursos.WebApp.Modulos.ModuloTurma.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Apresentacao;

public class MatriculaController(
    IRepositorioMatricula repositorioMatricula,
    IRepositorioTurma repositorioTurma,
    IRepositorioAluno repositorioAluno
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarMatriculaViewModel> listarVms = repositorioMatricula
            .SelecionarTodos()
            .Select(m => new ListarMatriculaViewModel(
                m.Id,
                m.Aluno.Nome,
                m.Aluno.NumeroMatricula,
                m.Turma.Nome
            ))
            .ToList();

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMatriculaViewModel cadastrarVm = new(TurmaId: null, AlunoId: null);

        CarregarTurmas();
        CarregarAlunos();

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMatriculaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarTurmas();
            CarregarAlunos();
            return View(cadastrarVm);
        }

        Turma? turma = repositorioTurma.SelecionarPorId(cadastrarVm.TurmaId!.Value);
        if (turma == null)
            ModelState.AddModelError(nameof(cadastrarVm.TurmaId), "Turma não encontrada.");

        Aluno? aluno = repositorioAluno.SelecionarPorId(cadastrarVm.AlunoId!.Value);
        if (aluno == null)
            ModelState.AddModelError(nameof(cadastrarVm.AlunoId), "Aluno não encontrado.");

        if (!ModelState.IsValid)
        {
            CarregarTurmas();
            CarregarAlunos();
            return View(cadastrarVm);
        }

        if (repositorioMatricula.Existe(turma!.Id, aluno!.Id))
        {
            ModelState.AddModelError(nameof(cadastrarVm.AlunoId), "Este aluno já está matriculado nesta turma.");
            CarregarTurmas();
            CarregarAlunos();
            return View(cadastrarVm);
        }

        if (repositorioMatricula.ContarPorTurmaId(turma.Id) >= turma.NumeroMaximoAlunos)
            ModelState.AddModelError(string.Empty, "A turma atingiu o número máximo de alunos.");

        if (!ModelState.IsValid)
        {
            CarregarTurmas();
            CarregarAlunos();
            return View(cadastrarVm);
        }

        repositorioMatricula.Cadastrar(new Matricula(aluno, turma));

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Matricula? matricula = repositorioMatricula.SelecionarPorId(id);

        if (matricula == null)
        {
            return RedirectToAction(nameof(Listar));
        }

        EditarMatriculaViewModel editarVm = new(
            matricula.Id,
            matricula.Turma.Id,
            matricula.Aluno.Id
        );

        CarregarTurmas();
        CarregarAlunos();

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarMatriculaViewModel editarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarTurmas();
            CarregarAlunos();
            return View(editarVm);
        }

        Matricula? matricula = repositorioMatricula.SelecionarPorId(editarVm.Id);
        if (matricula == null)
            ModelState.AddModelError(string.Empty, "Matrícula não encontrada.");

        Turma? turma = repositorioTurma.SelecionarPorId(editarVm.TurmaId!.Value);
        if (turma == null)
            ModelState.AddModelError(nameof(editarVm.TurmaId), "Turma não encontrada.");

        Aluno? aluno = repositorioAluno.SelecionarPorId(editarVm.AlunoId!.Value);
        if (aluno == null)
            ModelState.AddModelError(nameof(editarVm.AlunoId), "Aluno não encontrado.");

        if (!ModelState.IsValid)
        {
            CarregarTurmas();
            CarregarAlunos();
            return View(editarVm);
        }

        if (repositorioMatricula.Existe(turma!.Id, aluno!.Id, editarVm.Id))
        {
            ModelState.AddModelError(nameof(editarVm.AlunoId), "Este aluno já está matriculado nesta turma.");
            CarregarTurmas();
            CarregarAlunos();
            return View(editarVm);
        }

        int quantidadeMatriculas = repositorioMatricula.ContarPorTurmaId(turma.Id);
        if (matricula!.Turma.Id == turma.Id)
            quantidadeMatriculas--;

        if (quantidadeMatriculas >= turma.NumeroMaximoAlunos)
            ModelState.AddModelError(string.Empty, "A turma atingiu o número máximo de alunos.");

        if (!ModelState.IsValid)
        {
            CarregarTurmas();
            CarregarAlunos();
            return View(editarVm);
        }

        repositorioMatricula.Editar(editarVm.Id, new Matricula(aluno, turma));

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Matricula? matricula = repositorioMatricula.SelecionarPorId(id);

        if (matricula == null)
        {
            return RedirectToAction(nameof(Listar));
        }

        ExcluirMatriculaViewModel excluirVm = new(
            matricula.Id,
            matricula.Aluno.Nome,
            matricula.Aluno.NumeroMatricula,
            matricula.Turma.Nome
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirMatriculaViewModel excluirVm)
    {
        if (repositorioMatricula.SelecionarPorId(excluirVm.Id) != null)
            repositorioMatricula.Excluir(excluirVm.Id);

        return RedirectToAction(nameof(Listar));
    }

    private void CarregarTurmas()
    {
        ViewBag.Turmas = repositorioTurma
            .SelecionarTodos()
            .Select(t => new SelectListItem(t.Nome, t.Id.ToString()))
            .ToList();
    }

    private void CarregarAlunos()
    {
        ViewBag.Alunos = repositorioAluno
            .SelecionarTodos()
            .Select(a => new SelectListItem($"{a.Nome} ({a.NumeroMatricula})", a.Id.ToString()))
            .ToList();
    }
}
