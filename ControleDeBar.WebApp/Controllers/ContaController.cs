using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Dominio.ModuloProduto;
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
    private readonly ContextoDados contextoDados;
    private readonly RepositorioContaEmArquivo repositorioConta;
    private readonly RepositorioMesaEmArquivo repositorioMesa;
    private readonly RepositorioGarcomEmArquivo repositorioGarcom;
    private readonly RepositorioProdutoEmArquivo repositorioProduto;

    public ContaController()
    {
        contextoDados = new ContextoDados(carregarDados: true);
        
        repositorioConta = new RepositorioContaEmArquivo(contextoDados);
        repositorioMesa = new RepositorioMesaEmArquivo(contextoDados);
        repositorioGarcom = new RepositorioGarcomEmArquivo(contextoDados);
        repositorioProduto = new RepositorioProdutoEmArquivo(contextoDados);

    }

    [HttpGet]
    public IActionResult Index(string? status)
    {
        List<Conta> contas;

        switch (status)
        {
            case "abertas": contas = repositorioConta.SelecionarContasEmAberto(); break;
            case "fechadas": contas = repositorioConta.SelecionarContasFechadas(); break;
            default: contas = repositorioConta.SelecionarRegistros(); break;
        }

        VisualizarContasViewModel visualizarContasVm = new VisualizarContasViewModel(contas);

        return View(visualizarContasVm);
    }

    [HttpGet]
    public IActionResult Abrir()
    {
        List<Mesa> mesas = repositorioMesa.SelecionarRegistros();
        List<Garcom> garcons = repositorioGarcom.SelecionarRegistros();

        AbrirContaViewModel abrirVm = new AbrirContaViewModel(mesas,garcons);

        return View(abrirVm);
    }

    [HttpPost]
    public IActionResult Abrir(AbrirContaViewModel abrirVm)
    {
        if (!ModelState.IsValid)
            return View(abrirVm);

        Mesa mesaSelecionada = repositorioMesa.SelecionarRegistroPorId(abrirVm.MesaId);
        Garcom garconSelecionado = repositorioGarcom.SelecionarRegistroPorId(abrirVm.GarcomId);

        var conta = new Conta(abrirVm.Titular, mesaSelecionada, garconSelecionado);

        repositorioConta.CadastrarRegistro(conta);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Fechar(Guid id)
    {
        Conta contaSelecionada = repositorioConta.SelecionarRegistroPorId(id);

        FecharContaViewModel fecharContaVm = new FecharContaViewModel(
            contaSelecionada.Id,
            contaSelecionada.Titular,
            contaSelecionada.Mesa.Numero,
            contaSelecionada.Garcom.Nome,
            contaSelecionada.CalcularValorTotal(),
            contaSelecionada.Pedidos
        );

        return View(fecharContaVm);
    }

    [HttpPost]
    public IActionResult FecharConfirmado(Guid id)
    {
        Conta contaSelecionada = repositorioConta.SelecionarRegistroPorId(id);

        contaSelecionada.Fechar();

        contextoDados.Salvar();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult GerenciarPedidos(Guid id)
    {
        Conta contaSelecionada = repositorioConta.SelecionarRegistroPorId(id);

        List<Produto> produtos = repositorioProduto.SelecionarRegistros();

        GerenciarPedidosViewModel gerenciarPedidosVm = new GerenciarPedidosViewModel(
            contaSelecionada,
            produtos
        );

        return View(gerenciarPedidosVm);
    }

    [HttpPost]
    public IActionResult AdicionarPedido(Guid id, AdicionarPedidoViewModel adicionarPedidoVm)
    {
        Conta contaSelecionada = repositorioConta.SelecionarRegistroPorId(id);

        Produto produtoSelecionado = repositorioProduto.SelecionarRegistroPorId(adicionarPedidoVm.IdProduto);

        Pedido pedido = contaSelecionada.RegistrarPedido(produtoSelecionado, adicionarPedidoVm.QuantidadeSolicitada);

        contextoDados.Salvar();

        List<Produto> produtos = repositorioProduto.SelecionarRegistros();


        GerenciarPedidosViewModel gerenciarPedidosVm = new GerenciarPedidosViewModel(
            contaSelecionada,
            produtos
        );

        

        return View(nameof(GerenciarPedidos), gerenciarPedidosVm);
    }

    [HttpPost]
    public IActionResult RemoverPedido(Guid id, Guid idPedido)
    {
        Conta contaSelecionada = repositorioConta.SelecionarRegistroPorId(id);

        contaSelecionada.RemoverPedido(idPedido);

        contextoDados.Salvar();

        List<Produto> produtos = repositorioProduto.SelecionarRegistros();

        GerenciarPedidosViewModel gerenciarPedidosVm = new GerenciarPedidosViewModel(
            contaSelecionada,
            produtos
        );

        return View(nameof(GerenciarPedidos), gerenciarPedidosVm);
    }
}