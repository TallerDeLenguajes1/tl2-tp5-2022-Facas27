using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadeteriaWeb.Models.UsuarioModels;

namespace CadeteriaWeb.Models.UsuarioModels
{
    public interface IUsuario
    {
        Usuario Logear(string usuario, string pass);
    }
}