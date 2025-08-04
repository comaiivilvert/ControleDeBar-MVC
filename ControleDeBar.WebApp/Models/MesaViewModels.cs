using ControleDeBar.Dominio.ModuloMesa;
using System.ComponentModel.DataAnnotations;

namespace ControleDeBar.WebApp.Models
{
    public class VisualizarMesasViewModel
    
    {
        public List<DetalhesMesaViewModel> Registros { get; set; } = new List<DetalhesMesaViewModel>();


        public VisualizarMesasViewModel(List<Mesa> mesas)
        {
            foreach (Mesa m in mesas)
            {
                DetalhesMesaViewModel detalhesVm = new DetalhesMesaViewModel(
                    m.Id,
                    m.Numero,
                    m.Capacidade);


                Registros.Add(detalhesVm);


            }

        }
    }
        public class DetalhesMesaViewModel
        {
            public Guid Id { get; set; }
            public int Numero { get; set; }
            public int Capacidade { get; set; }

            public DetalhesMesaViewModel(Guid id, int numero, int capacidade)
            {
                Id = id;
                Numero = numero;
                Capacidade = capacidade;
            }
        }

    public class CadastrarMesaViewModel
    {

        [Required(ErrorMessage = "O campo \"Numero\" é obrigatorio.")]
        [Range(1,1000, ErrorMessage = "O campo \"Numero\" precisa conter um valor entre 1 e 1000.")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O campo \"Numero\" é obrigatorio.")]
        [Range(1, 1000, ErrorMessage = "O campo \"Capacidade\" precisa conter um valor entre 1 e 1000.")]
        public int Capacidade { get; set; }

        public CadastrarMesaViewModel()
        {
        }
    }

    public class EditarMesaViewModel
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo \"Numero\" é obrigatorio.")]
        [Range(1, 1000, ErrorMessage = "O campo \"Numero\" precisa conter um valor entre 1 e 1000.")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O campo \"Numero\" é obrigatorio.")]
        [Range(1, 1000, ErrorMessage = "O campo \"Capacidade\" precisa conter um valor entre 1 e 1000.")]
        public int Capacidade { get; set; }

        public EditarMesaViewModel()
        {
        }

        public EditarMesaViewModel(Guid id, int numero, int capacidade)
        {
            Id = id;
            Numero = numero;
            Capacidade = capacidade;
        }
    }


    public class ExcluirMesaViewModel
    {
        public Guid Id { get; set; }
        public int Numero { get; set; }

        public ExcluirMesaViewModel() { }

        public ExcluirMesaViewModel(Guid id, int numero)
        {
            Id = id;
            Numero = numero;
        }
    }


}