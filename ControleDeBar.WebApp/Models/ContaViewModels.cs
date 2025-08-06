using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using System.ComponentModel.DataAnnotations;

namespace ControleDeBar.WebApp.Models;

public class CadastrarContaViewModel
{

    public string Titular { get; set; }

    public Guid MesaId { get; set; }
    public Guid GarcomId { get; set; } 

    public List<SelecionarMesaViewModel> MesasDisponiveis { get; set; }
    public List<SelecionarGarcomViewModel> GarconsDisponiveis { get; set; }

    

    public CadastrarContaViewModel()
    {
        MesasDisponiveis = new List<SelecionarMesaViewModel>();
        GarconsDisponiveis = new List<SelecionarGarcomViewModel>();
    }

    public CadastrarContaViewModel(List<Mesa> mesas, List<Garcom> garcom) : this()
    {
    

        foreach (Mesa m in mesas)
        {
            SelecionarMesaViewModel selecionarVm =
                new SelecionarMesaViewModel(m.Id, m.Numero);

            MesasDisponiveis.Add(selecionarVm);
        }
        foreach (Garcom g in garcom)
        {
            SelecionarGarcomViewModel selecionarVm =
                new SelecionarGarcomViewModel(g.Id, g.Nome);

            GarconsDisponiveis.Add(selecionarVm);
        }
    }


}
public class EditarContaViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo \"Titular\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Titular\" deve conter ao menos 2 caracteres.")]
    public string Titular { get; set; }

    [Required(ErrorMessage = "O campo \"Mesa\" é obrigatório.")]
    public Mesa mesa { get; set; }

    [Required(ErrorMessage = "O campo \"Garcom\" é obrigatório.")]
    public Garcom garcom { get; set; }

    //public Pedido[] Pedidos { get; set; }
    public EditarContaViewModel()
    {
    }

    public EditarContaViewModel(Guid id, string titular, Mesa mesa, Garcom garcom)
    {
        Id = id;
        Titular = titular;
        this.mesa = mesa;
        this.garcom = garcom;
    }
}

public class ExcluirContaViewModel
{
    public Guid Id { get; set; }
    public string Titular { get; set; }

    public ExcluirContaViewModel()
    {
    }

    public ExcluirContaViewModel(Guid id, string Titular)
    {
        Id = id;
        Titular = Titular;
    }
}

public class VisualizarContasViewModel
{
    public List<DetalhesContaViewModel> Registros { get; set; } = new List<DetalhesContaViewModel>();

    public VisualizarContasViewModel(List<Conta> garcons)
    {
        foreach (Conta Conta in garcons)
        {
            DetalhesContaViewModel detalhesVm = new DetalhesContaViewModel(
                Conta.Id,
                Conta.Titular,
                Conta.EstaAberta
            );

            Registros.Add(detalhesVm);
        }
    }
}

public class DetalhesContaViewModel
{
    public Guid Id { get; set; }
    public string Titular { get; set; }
    public bool EstaAberta { get; set; }

    public DetalhesContaViewModel(Guid id, string titular, bool estaAberta)
    {
        Id = id;
        Titular = titular;
        EstaAberta = estaAberta;
    }
}

public class SelecionarMesaViewModel
{
    public Guid Id { get; set; }
    public int Numero { get; set; }

    public SelecionarMesaViewModel(Guid id, int numero)
    {
        Id = id;
        Numero = numero;
    }
}

public class SelecionarGarcomViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }

    public SelecionarGarcomViewModel(Guid id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}