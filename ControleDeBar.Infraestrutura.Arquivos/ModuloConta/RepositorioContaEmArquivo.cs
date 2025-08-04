using ControleDeBar.Dominio.ModuloConta;
using ControleDeBar.Infraestrutura.Arquivos.Compartilhado;

namespace ControleDeBar.Infraestrutura.Arquivos.ModuloConta;

public class RepositorioContaEmArquivo : RepositorioBaseEmArquivo<Conta>
{
    public RepositorioContaEmArquivo(ContextoDados contextoDados) : base(contextoDados)
    {
    }

    protected override List<Conta> ObterRegistros()
    {
        return contextoDados.Contas;
    }
}