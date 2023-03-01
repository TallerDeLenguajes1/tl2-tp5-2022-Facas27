using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CadeteriaWeb.Models.CadetesModels;

namespace CadeteriaWeb.ViewModels
{
    public class PedidosViewModels
    {
        public int Nro { get;  set; }

        [Required (ErrorMessage = "La Observacion es un campo obligatorio")][StringLength(100)][Display(Name="Observaciones del Pedido")]
        public string Obs { get; set; }
        
        [Required][Display(Name="Nombre del Cliente ")]
        public int Cliente {get;set;}
        
        [Display(Name="Cadete del Pedido")]
        public int Cadete {get;set;}

        [Required]
        public bool Estado {get;set;}


        public PedidosViewModels(){
            
            this.Estado = true;
        }
        public PedidosViewModels(int nro, string obs,int cliente, int cadete){
            Nro = nro;
            Obs = obs;
            Cliente = cliente;
            Cadete = cadete;
            Estado = true;


        }
        

    }
}