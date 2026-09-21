using EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Modulos.ModuloInstrutor.Apresentacao;

public class InstrutorController(
    IRepositorioInstrutor repositorioInstrutor
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarInstrutorViewModel> listarVms = repositorioInstrutor
            .SelecionarTodos()
            .Select(i => new ListarInstrutorViewModel(
                i.Id,
                i.Nome,
                i.Telefone,
                i.Cpf
            ))
            .ToList();

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarInstrutorViewModel cadastrarVm = new CadastrarInstrutorViewModel(
            string.Empty,
            string.Empty,
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarInstrutorViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        if (repositorioInstrutor.ExisteComNome(cadastrarVm.Nome))
        {
            ModelState.AddModelError(nameof(cadastrarVm.Nome), "Já existe um instrutor com este nome.");
            return View(cadastrarVm);
        }

        Instrutor novoInstrutor = new Instrutor(
            cadastrarVm.Nome,
            cadastrarVm.Telefone,
            cadastrarVm.Cpf
        );
        List<string> erros = novoInstrutor.Validar();

        if (erros.Count > 0)
        {
            ModelState.AddModelError(string.Empty, erros.First());
            return View(cadastrarVm);
        }

        repositorioInstrutor.Cadastrar(novoInstrutor);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(id);

        if (instrutor == null)
            return RedirectToAction(nameof(Listar));

        EditarInstrutorViewModel editarVm = new EditarInstrutorViewModel(
            instrutor.Id,
            instrutor.Nome,
            instrutor.Telefone,
            instrutor.Cpf
        );

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarInstrutorViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        if (repositorioInstrutor.ExisteComNome(editarVm.Nome, editarVm.Id))
        {
            ModelState.AddModelError(nameof(editarVm.Nome), "Já existe um instrutor com este nome.");
            return View(editarVm);
        }

        Instrutor instrutorAtualizado = new Instrutor(
            editarVm.Nome,
            editarVm.Telefone,
            editarVm.Cpf
        );
        List<string> erros = instrutorAtualizado.Validar();

        if (erros.Count > 0)
        {
            ModelState.AddModelError(string.Empty, erros.First());
            return View(editarVm);
        }

        if (!repositorioInstrutor.Editar(editarVm.Id, instrutorAtualizado))
        {
            ModelState.AddModelError(string.Empty, "Instrutor não encontrado.");
            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(id);

        if (instrutor == null)
            return RedirectToAction(nameof(Listar));

        ExcluirInstrutorViewModel excluirVm = new ExcluirInstrutorViewModel(
            instrutor.Id,
            instrutor.Nome,
            instrutor.Telefone,
            instrutor.Cpf
        );

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirInstrutorViewModel excluirVm)
    {
        Instrutor? instrutor = repositorioInstrutor.SelecionarPorId(excluirVm.Id);

        if (instrutor == null)
            return RedirectToAction(nameof(Listar));

        repositorioInstrutor.Excluir(excluirVm.Id);

        return RedirectToAction(nameof(Listar));
    }
}
