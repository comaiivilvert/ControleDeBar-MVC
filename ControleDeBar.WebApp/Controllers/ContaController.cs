using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloConta;
using ControleDeBar.Infraestrutura.Arquivos.ModuloGarcom;
using ControleDeBar.Infraestrutura.Arquivos.ModuloMesa;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControleDeBar.WebApp.Controllers;

public class ContaController : Controller
{
    private readonly RepositorioContaEmArquivo repositorioConta;
    private readonly RepositorioMesaEmArquivo repositorioMesa;
    private readonly RepositorioGarcomEmArquivo repositorioGarcom;
    private readonly RepositorioProdutoEmArquivo repositorioProduto;

    public ContaController()
    {
        ContextoDados contextoDados = new ContextoDados(carregarDados: true);
        
        repositorioConta = new RepositorioContaEmArquivo(contextoDados);
        repositorioMesa = new RepositorioMesaEmArquivo(contextoDados);
        repositorioGarcom = new RepositorioGarcomEmArquivo(contextoDados);
        repositorioProduto = new RepositorioProdutoEmArquivo(contextoDados);

    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Conta> Conta = repositorioConta.SelecionarRegistros();

        VisualizarContasViewModel visualizarVm = new VisualizarContasViewModel(Conta);

        return View(visualizarVm);
    }

    [HttpGet]
    public IActionResult Cadastrar()
    {
        List<Mesa> mesas = repositorioMesa.SelecionarRegistros();
        List<Garcom> garcons = repositorioGarcom.SelecionarRegistros();

        CadastrarContaViewModel cadastrarVm = new CadastrarContaViewModel(mesas,garcons);

        return View(cadastrarVm);
    }

    [HttpPost]
    public IActionResult Cadastrar(CadastrarContaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        Mesa mesaSelecionada = repositorioMesa.SelecionarRegistroPorId(cadastrarVm.MesaId);
        Garcom garconSelecionado = repositorioGarcom.SelecionarRegistroPorId(cadastrarVm.GarcomId);

        var entidade = new Conta(cadastrarVm.Titular, mesaSelecionada, garconSelecionado);

        repositorioConta.CadastrarRegistro(entidade);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(Guid id)
    {
        var registro = repositorioConta.SelecionarRegistroPorId(id);

        EditarContaViewModel editarVm = new EditarContaViewModel(
            id,
            registro.Titular,
            registro.Mesa,
            registro.Garcom
        );

        return View(editarVm);
    }

    [HttpPost]
    public IActionResult Editar(EditarContaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        var ContaEditado = new Conta(editarVm.Titular, editarVm.mesa, editarVm.garcom);

        repositorioConta.EditarRegistro(editarVm.Id, ContaEditado);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Excluir(Guid id)
    {
        var registro = repositorioConta.SelecionarRegistroPorId(id);

        ExcluirContaViewModel excluirVm = new ExcluirContaViewModel(
            id,
            registro.Titular
        );

        return View(excluirVm);
    }

    [HttpPost]
    public IActionResult Excluir(ExcluirContaViewModel excluirVm)
    {
        repositorioConta.ExcluirRegistro(excluirVm.Id);

        return RedirectToAction(nameof(Index));
    }
}