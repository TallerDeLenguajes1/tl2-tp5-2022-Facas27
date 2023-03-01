using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models.ClientesModels
{
    public interface IClientesRepositorio
    {
        Clientes ClientePorID(int id);

        List<Clientes>TodosCliente();

        bool SubirClientes(Clientes cliente);
        bool EliminarClientes(string id);

        bool ActualizarClientes(Clientes Cliente);

        
    }
}