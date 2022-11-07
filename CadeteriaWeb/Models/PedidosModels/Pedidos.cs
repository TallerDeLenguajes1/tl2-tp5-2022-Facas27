using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadeteriaWeb.Models.CadetesModels;
using CadeteriaWeb.Models.ClientesModels;

namespace CadeteriaWeb.Models.PedidosModels;
public class Pedidos
{
    public int Nro { get; set; }

    public string Obs { get; set; }

    public int Cliente {get;set;}
    
    public int? Cadete {get;set;}

    public bool Estado {get;set;}



    public Pedidos(){}

    public Pedidos(int nro , string obs,bool estado, int cliente, int cadete)
    {
        
        Obs = obs;
        Estado = estado;
        Cliente = cliente;
        Cadete = cadete;
        Nro = nro;
        
    }
    public void CambiarEstado(){
        Estado = false;
    }
}
