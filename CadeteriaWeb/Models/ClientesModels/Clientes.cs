using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models.ClientesModels
{
    public class Clientes
    {
        
        public int ID { get; set; }

        public string Nombre {get;set;}

        public string Direccion {get;set;}

        public string Telefono {get;set;}

        public Clientes(){}

        public Clientes(int iD, string nombre, string direccion, string telefono)
        {
            ID = iD;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }


        
        
    }
}