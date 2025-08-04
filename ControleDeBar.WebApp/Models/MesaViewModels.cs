﻿using ControleDeBar.Dominio.ModuloMesa;
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
            public int Id { get; set; }
            public int Numero { get; set; }
            public int Capacidade { get; set; }

            public DetalhesMesaViewModel(int id, int numero, int capacidade)
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

        public int Id { get; set; }

        [Required(ErrorMessage = "O campo \"Numero\" é obrigatorio.")]
        [Range(1, 1000, ErrorMessage = "O campo \"Numero\" precisa conter um valor entre 1 e 1000.")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O campo \"Numero\" é obrigatorio.")]
        [Range(1, 1000, ErrorMessage = "O campo \"Capacidade\" precisa conter um valor entre 1 e 1000.")]
        public int Capacidade { get; set; }

        public EditarMesaViewModel()
        {
        }

        public EditarMesaViewModel(int id, int numero, int capacidade)
        {
            Id = id;
            Numero = numero;
            Capacidade = capacidade;
        }
    }


    public class ExcluirMesaViewModel
    {
        public int Id { get; set; }
        public int Numero { get; set; }

        public ExcluirMesaViewModel() { }

        public ExcluirMesaViewModel(int id, int numero)
        {
            Id = id;
            Numero = numero;
        }
    }


}