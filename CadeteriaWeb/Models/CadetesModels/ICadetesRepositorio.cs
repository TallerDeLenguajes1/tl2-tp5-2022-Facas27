using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models.CadetesModels;

public interface ICadetesRepositorio
{
    Cadetes CadetePorID(int id);

    List<Cadetes>TodosCadetes();
    public bool EliminarCadetes(string ID);
    public bool SubirCadetes(Cadetes Cadete);

    public bool ActualizarCadetes(Cadetes Cadete);

    
}
