using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Modulos.ModuloAluno.Apresentacao;

public class AlunoController(
    IRepositorioAluno repositorioAluno
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarAlunoViewModel> listarVms = repositorioAluno
            .SelecionarTodos()
            .Select(a => new ListarAlunoViewModel(
                a.Id,
                a.Nome,
                a.Email,
                a.NumeroMatricula
            ))
            .ToList();

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarAlunoViewModel cadastrarVm = new CadastrarAlunoViewModel(
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarAlunoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        Aluno novoAluno = new Aluno(cadastrarVm.Nome, cadastrarVm.Email);
        List<string> erros = novoAluno.Validar();

        if (erros.Count > 0)
        {
            ModelState.AddModelError(string.Empty, erros.First());
            return View(cadastrarVm);
        }

        repositorioAluno.Cadastrar(novoAluno);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Aluno? aluno = repositorioAluno.SelecionarPorId(id);

        if (aluno == null)
            return RedirectToAction(nameof(Listar));

        EditarAlunoViewModel editarVm = new EditarAlunoViewModel(
            aluno.Id,
            aluno.Nome,
            aluno.Email
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarAlunoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        Aluno alunoAtualizado = new Aluno(editarVm.Nome, editarVm.Email);
        List<string> erros = alunoAtualizado.Validar();

        if (erros.Count > 0)
        {
            ModelState.AddModelError(string.Empty, erros.First());
            return View(editarVm);
        }

        if (!repositorioAluno.Editar(editarVm.Id, alunoAtualizado))
        {
            ModelState.AddModelError(string.Empty, "Aluno não encontrado.");
            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Aluno? aluno = repositorioAluno.SelecionarPorId(id);

        if (aluno == null)
            return RedirectToAction(nameof(Listar));

        ExcluirAlunoViewModel excluirVm = new ExcluirAlunoViewModel(
            aluno.Id,
            aluno.Nome,
            aluno.Email,
            aluno.NumeroMatricula
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirAlunoViewModel excluirVm)
    {
        Aluno? aluno = repositorioAluno.SelecionarPorId(excluirVm.Id);

        if (aluno == null)
            return RedirectToAction(nameof(Listar));

        repositorioAluno.Excluir(excluirVm.Id);

        return RedirectToAction(nameof(Listar));
    }
}
