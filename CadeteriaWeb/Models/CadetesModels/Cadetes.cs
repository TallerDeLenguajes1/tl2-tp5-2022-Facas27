using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadeteriaWeb.Models.PedidosModels;
using CadeteriaWeb.ViewModels;


namespace CadeteriaWeb.Models.CadetesModels;

public class Cadetes
{
    
    
    public int ID {get;set;}
    public string Nombre { get; set; }

    public string Direccion { get; set; }

    public string Telefono { get; set; }

    public int IDCadeteria{get;set;}


    public Cadetes(){}

    

    
    public Cadetes(int id,string nombre, string direccion, string telefono, int idcad)
    {
        
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        ID = id;
        IDCadeteria = idcad;
    }

    


    
    

    


    
}
