using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models.UsuarioModels
{
    public enum RolUsuario
    {
        Administrador,
        Cadete
    }
     public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Usu { get; set; }
        public RolUsuario Rol { get; set; }
        public int IDCadete { get; set; }

        public Usuario()
        {

        }

        public Usuario(int id, string name, string user, RolUsuario rol, int iDCadete = 0)
        {
            ID = id;
            Nombre = name;
            Usu = user;
            Rol = rol;
            IDCadete = iDCadete;
        }
    }
}
  
   