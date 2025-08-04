using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;
using ControleDeBar.Infraestrutura.Arquivos.ModuloMesa;
using ControleDeBar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace ControleDeBar.WebApp.Controllers
{
    public class MesaController : Controller
    {

        private readonly RepositorioMesaEmArquivo repositorioMesa;

        public MesaController()
        {
            ContextoDados contexto = new ContextoDados(carregarDados: true);
            repositorioMesa = new RepositorioMesaEmArquivo(contexto);
        }

        public IActionResult Index()
        {

            List<Mesa> mesas = repositorioMesa.SelecionarRegistros();
            VisualizarMesasViewModel visualizarVm = new VisualizarMesasViewModel(mesas);

            return View(visualizarVm);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            CadastrarMesaViewModel cadastrarVm = new CadastrarMesaViewModel();

            return View(cadastrarVm);
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarMesaViewModel cadastrarVm)
        {
            if (!ModelState.IsValid)
            {
                return View(cadastrarVm);
            }

            var entidade = new Mesa(cadastrarVm.Numero, cadastrarVm.Capacidade);
            repositorioMesa.CadastrarRegistro(entidade);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Editar(Guid id)
        {
            var registro = repositorioMesa.SelecionarRegistroPorId(id);

            EditarMesaViewModel editarVm = new EditarMesaViewModel(
                id,
                registro.Numero,
                registro.Capacidade
                );

            return View(editarVm);
        }

        [HttpPost]
        public ActionResult Editar(EditarMesaViewModel editarVm)
        {
            if (!ModelState.IsValid)
            { return View(editarVm); }

            var mesaEditada = new Mesa(editarVm.Numero, editarVm.Capacidade);
            repositorioMesa.EditarRegistro(editarVm.Id, mesaEditada);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Excluir(Guid id)
        {
            var registro = repositorioMesa.SelecionarRegistroPorId(id);

            ExcluirMesaViewModel excluirVm = new ExcluirMesaViewModel(
                id,
                registro.Numero
                );

            return View(excluirVm);
        }

        [HttpPost]
        public ActionResult Excluir(ExcluirMesaViewModel excluirVm)
        {
            repositorioMesa.ExcluirRegistro(excluirVm.Id);

            return RedirectToAction(nameof(Index));
        }


    }
    }
