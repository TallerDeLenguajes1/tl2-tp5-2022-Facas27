using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadeteriaWeb.Models.CadetesModels;

namespace CadeteriaWeb.Models.PedidosModels
{
    public class PedidosRepositorio:IPedidos
    {
        private List<Pedidos>ListaPedidos;

        public PedidosRepositorio(){
            CadetesRepositorio CadeRepo = new CadetesRepositorio();
            ListaPedidos = new List<Pedidos>();
            string path = @"Pedidos.csv";
            string[]leer = System.IO.File.ReadAllLines(path);
            foreach (var item in leer)
            {
                string[]datos = item.Split(";");
                                                           //Nro,    Obs             IDCliente            IDCadete
                ListaPedidos.Add(new Pedidos(Int32.Parse(datos[0]),datos[1],Convert.ToBoolean(datos[2]),Int32.Parse(datos[3]), Int32.Parse(datos[4])));
            }

            
        }

        public Pedidos PedidoPorNro(int nro){
            
            return this.ListaPedidos.FirstOrDefault(e => e.Nro == nro);

        }

        public List<Pedidos> TodosPedidos(){
            return this.ListaPedidos;
        }
        public void SubirPedido(Pedidos Pedido){
            string path = @"Pedidos.csv";
            ListaPedidos.Add(Pedido);
            File.WriteAllText(path,"");

            foreach (var item in ListaPedidos)
            {
                string estado;
                string cad = $"{item.Nro};{item.Obs};{item.Estado};{item.Cliente};{item.Cadete}\n";
                File.AppendAllText(path, cad);
            }

        }
        public bool EliminarPedido(string Nro){
            string path = @"Pedidos.csv";
            string[]leer = System.IO.File.ReadAllLines(path);
            System.IO.File.WriteAllText(path,"");
            bool band = false;
            for (int i = 0; i < leer.Length; i++)
            {
                string[]datos = leer[i].Split(";");
                if ( Nro == datos[0].Trim())
                {
                  band = true;   
                }else
                {
                    System.IO.File.AppendAllText(path, leer[i] +"\n");
                }

                
            }
            return band;
           


        } 


    }
}