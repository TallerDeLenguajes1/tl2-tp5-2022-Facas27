using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models.PedidosModels
{
    public interface IPedidos
    {
        public Pedidos PedidoPorNro(int nro);

        public List<Pedidos> TodosPedidos();
        public bool SubirPedido(Pedidos Pedido);
         public bool EliminarPedido(string Nro);


    }
}