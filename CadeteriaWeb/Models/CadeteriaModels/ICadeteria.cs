using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadeteriaWeb.Models.CadeteriaModels;

namespace CadeteriaWeb.Models
{
    public interface ICadeteria
    {
      Cadeteria CadeteriaPorID(int id);

      List<Cadeteria>TodosCadeteria();

    }
}