using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloProduto;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeBar.Infraestrutura.Arquivos.ModuloGarcom;

public class RepositorioProdutoEmArquivo : RepositorioBaseEmArquivo<Produto>
{
    public RepositorioProdutoEmArquivo(ContextoDados contextoDados) : base(contextoDados)
    {
    }

    protected override List<Produto> ObterRegistros()
    {
        return contextoDados.Produtos;
    }
}