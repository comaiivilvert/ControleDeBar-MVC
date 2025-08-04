using ControleDeBar.Dominio.ModuloProduto;
using System.ComponentModel.DataAnnotations;

namespace ControleDeBar.WebApp.Models;

public class CadastrarProdutoViewModel
{
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Nome\" deve conter ao menos 2 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo \"Valor\" é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Valor\" deve ser maior que zero.")]
    public decimal Valor { get; set; }

    public CadastrarProdutoViewModel()
    {
    }
}
public class EditarProdutoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório.")]
    [MinLength(2, ErrorMessage = "O campo \"Nome\" deve conter ao menos 2 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo \"Valor\" é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Valor\" deve ser maior que zero.")]
    public decimal Valor { get; set; }

    public EditarProdutoViewModel()
    {
    }

    public EditarProdutoViewModel(int id, string nome, decimal valor)
    {
        Id = id;
        Nome = nome;
        Valor = valor;
    }
}

public class ExcluirProdutoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirProdutoViewModel()
    {
    }

    public ExcluirProdutoViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarProdutoViewModel
{
    public List<DetalhesProdutoViewModel> Registros { get; set; } = new List<DetalhesProdutoViewModel>();

    public VisualizarProdutoViewModel(List<Produto> produtos)
    {
        foreach (Produto produto in produtos)
        {
            DetalhesProdutoViewModel detalhesVm = new DetalhesProdutoViewModel(
                produto.Id,
                produto.Nome,
                produto.Valor
            );

            Registros.Add(detalhesVm);
        }
    }
}

public class DetalhesProdutoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }

    public DetalhesProdutoViewModel(int id, string nome, decimal valor)
    {
        Id = id;
        Nome = nome;
        Valor = valor;
    }
}