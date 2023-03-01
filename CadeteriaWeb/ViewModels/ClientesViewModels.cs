using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CadeteriaWeb.ViewModels
{
    public class ClientesViewModels
    {
        

        public int ID {get;set;}

        [Required (ErrorMessage = "El Nombre es un campo obligatorio")][StringLength(100)][Display(Name="Nombre del Cliente")]
        public string Nombre{get;set;}

        [Required(ErrorMessage = "La direccion es un campo obligatorio")][StringLength(100)][Display(Name="Direccion del Cliente")]
        public string Direccion {get;set;}

        [Required (ErrorMessage = "La direccion es un campo obligatorio")][Phone][Display(Name="Telefono del Cliente")]
        public string  Telefono {get;set;}

        public ClientesViewModels(){}


        public ClientesViewModels(int iD, string nombre, string direccion, string telefono)
        {
            ID = iD;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }
        

    }

    
}