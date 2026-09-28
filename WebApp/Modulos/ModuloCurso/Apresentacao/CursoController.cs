using EscolaDeCursos.WebApp.Modulos.ModuloCurso.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.Apresentacao;

public class CursoController(
    IRepositorioCurso repositorioCurso,
    IRepositorioAula repositorioAula
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarCursoViewModel> listarVms = repositorioCurso
            .SelecionarTodos()
            .Select(c => new ListarCursoViewModel(
                c.Id,
                c.Nome,
                c.Nivel,
                c.CargaHoraria
            ))
            .ToList();

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarCursoViewModel cadastrarVm = new(
            string.Empty,
            null,
            null
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarCursoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        if (repositorioCurso.ExisteComNome(cadastrarVm.Nome))
        {
            ModelState.AddModelError(
                nameof(cadastrarVm.Nome),
                "Já existe um curso com este nome."
            );

            return View(cadastrarVm);
        }

        Curso novoCurso = new(
            cadastrarVm.Nome,
            cadastrarVm.Nivel!.Value,
            cadastrarVm.CargaHoraria!.Value
        );

        List<string> erros = novoCurso.Validar();

        if (erros.Count > 0)
        {
            ModelState.AddModelError(string.Empty, erros[0]);

            return View(cadastrarVm);
        }

        repositorioCurso.Cadastrar(novoCurso);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Curso? curso = repositorioCurso.SelecionarPorId(id);

        if (curso == null)
        {
            return RedirectToAction(nameof(Listar));
        }

        EditarCursoViewModel editarVm = new(
            curso.Id,
            curso.Nome,
            curso.Nivel,
            curso.CargaHoraria
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarCursoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        if (repositorioCurso.ExisteComNome(editarVm.Nome, editarVm.Id))
        {
            ModelState.AddModelError(
                nameof(editarVm.Nome),
                "Já existe um curso com este nome."
            );

            return View(editarVm);
        }

        Curso cursoAtualizado = new(
            editarVm.Nome,
            editarVm.Nivel!.Value,
            editarVm.CargaHoraria!.Value
        );

        List<string> erros = cursoAtualizado.Validar();

        if (erros.Count > 0)
        {
            ModelState.AddModelError(string.Empty, erros[0]);

            return View(editarVm);
        }

        if (!repositorioCurso.Editar(editarVm.Id, cursoAtualizado))
        {
            ModelState.AddModelError(string.Empty, "Curso não encontrado.");

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Curso? curso = repositorioCurso.SelecionarPorId(id);

        if (curso == null)
        {
            return RedirectToAction(nameof(Listar));
        }

        ExcluirCursoViewModel excluirVm = new(
            curso.Id,
            curso.Nome,
            curso.Nivel.ToString(),
            curso.CargaHoraria
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirCursoViewModel excluirVm)
    {
        Curso? curso = repositorioCurso.SelecionarPorId(excluirVm.Id);

        if (curso != null
            && !repositorioAula.ExistePorCursoId(excluirVm.Id))
            repositorioCurso.Excluir(excluirVm.Id);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult GerenciarAulas(Guid id)
    {
        Curso? curso = repositorioCurso.SelecionarPorId(id);

        if (curso == null)
        {
            return RedirectToAction(nameof(Listar));
        }

        List<ListarAulaViewModel> aulasVm = curso.Aulas
            .Select(a => new ListarAulaViewModel(
                a.Id,
                a.Nome,
                a.DuracaoEmMinutos,
                a.Ordem
            ))
            .ToList();

        GerenciarAulasViewModel gerenciarVm = new(
            curso.Id,
            curso.Nome,
            curso.Nivel.ToString(),
            curso.CargaHoraria,
            aulasVm
        );

        return View(gerenciarVm);
    }

    [HttpPost]
    public ActionResult AdicionarAula(AdicionarAulaViewModel adicionarVm)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(GerenciarAulas), new { id = adicionarVm.CursoId });

        if (repositorioAula.ExisteComNome(adicionarVm.CursoId, adicionarVm.Nome))
            return RedirectToAction(nameof(GerenciarAulas), new { id = adicionarVm.CursoId });

        if (repositorioAula.ExisteComOrdem(adicionarVm.CursoId, adicionarVm.Ordem!.Value))
            return RedirectToAction(nameof(GerenciarAulas), new { id = adicionarVm.CursoId });

        Curso? curso = repositorioCurso.SelecionarPorId(adicionarVm.CursoId);

        if (curso == null)
        {
            return RedirectToAction(nameof(GerenciarAulas), new { id = adicionarVm.CursoId });
        }

        Aula novaAula = new(
            adicionarVm.Nome,
            adicionarVm.DuracaoEmMinutos!.Value,
            adicionarVm.Ordem.Value,
            curso
        );

        List<string> erros = novaAula.Validar();

        if (erros.Count > 0)
        {
            return RedirectToAction(nameof(GerenciarAulas), new { id = adicionarVm.CursoId });
        }

        repositorioAula.Cadastrar(novaAula);

        return RedirectToAction(nameof(GerenciarAulas), new { id = adicionarVm.CursoId });
    }

    [HttpPost]
    public ActionResult EditarAula(EditarAulaViewModel editarVm)
    {
        Aula? aulaPersistida = repositorioAula.SelecionarPorId(editarVm.Id);

        if (aulaPersistida == null)
        {
            return RedirectToAction(nameof(GerenciarAulas), new { id = editarVm.CursoId });
        }

        Guid cursoId = aulaPersistida.Curso.Id;

        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(GerenciarAulas), new { id = cursoId });
        }

        if (repositorioAula.ExisteComNome(editarVm.CursoId, editarVm.Nome, editarVm.Id))
        {
            return RedirectToAction(nameof(GerenciarAulas), new { id = cursoId });
        }

        if (repositorioAula.ExisteComOrdem(cursoId, editarVm.Ordem!.Value, editarVm.Id))
        {
            return RedirectToAction(nameof(GerenciarAulas), new { id = cursoId });
        }

        Aula aulaAtualizada = new(
            editarVm.Nome,
            editarVm.DuracaoEmMinutos!.Value,
            editarVm.Ordem.Value,
            aulaPersistida.Curso
        );

        List<string> erros = aulaAtualizada.Validar();

        if (erros.Count > 0)
        {
            return RedirectToAction(nameof(GerenciarAulas), new { id = cursoId });
        }

        repositorioAula.Editar(editarVm.Id, aulaAtualizada);

        return RedirectToAction(nameof(GerenciarAulas), new { id = cursoId });
    }

    [HttpPost]
    public ActionResult RemoverAula(RemoverAulaViewModel removerVm)
    {
        Aula? aula = repositorioAula.SelecionarPorId(removerVm.Id);

        if (aula == null)
        {
            return RedirectToAction(nameof(GerenciarAulas), new { id = removerVm.CursoId });
        }

        Guid cursoId = aula.Curso.Id;

        repositorioAula.Excluir(removerVm.Id);

        return RedirectToAction(nameof(GerenciarAulas), new { id = cursoId });
    }
}
