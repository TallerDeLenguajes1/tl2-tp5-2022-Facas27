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
        public int Nro { get; private set; }

        [Required][StringLength(100)][Display(Name="Observaciones del Pedido")]
        public string Obs { get; set; }
        
        [Required][Display(Name="IDCliente del Pedido")]
        public int Cliente {get;set;}
        
        [Required][Display(Name="Cadete del Pedido")]
        public int Cadete {get;set;}

        public bool Estado {get;set;}


        public PedidosViewModels(){
            this.Nro = 0;
            foreach (string pedido in File.ReadAllLines(@"Pedidos.csv"))
            {
                if (pedido != "")
                {
                    this.Nro = Int32.Parse(pedido.Split(";")[0])+1;
                }
            }
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