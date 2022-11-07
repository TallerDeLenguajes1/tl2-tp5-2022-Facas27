using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models.ClientesModels
{
    public interface IClientes
    {
        Clientes ClientePorID(int id);

       List<Clientes>TodosCliente();

        
    }
}