using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadeteriaWeb.Models.UsuarioModels;

namespace CadeteriaWeb.ViewModels
{
    public class UsuarioViewModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Usu { get; set; }
        public RolUsuario Rol {get; set; }

        public UsuarioViewModel()
        {

        }

        public UsuarioViewModel(int id, string name, string user, RolUsuario rol)
        {
            ID = id;
            Nombre = name;
            Usu = user;
            Rol = rol;
        }
    }
     
    
}