using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadeteriaWeb.Models.CadetesModels;
using Microsoft.Data.Sqlite;

namespace CadeteriaWeb.Models.PedidosModels
{
    public class PedidosRepositorio:IPedidos
    {
        private List<Pedidos>ListaPedidos;
        static public SqliteConnection conexion = new SqliteConnection($"Filename=CadeteriaWeb.db");

        public PedidosRepositorio(){
            ListaPedidos = new List<Pedidos>();
            conexion.Open();
           SqliteCommand select = new SqliteCommand("SELECT * FROM pedidos", conexion);
           var query = select.ExecuteReader();
           while (query.Read())
            {                                          //ID,          OBS             Estado               IDCLIENTE        IDCADETE         
                ListaPedidos.Add(new Pedidos{Nro = query.GetInt32(0),
                                            Obs =  query.GetString(1),
                                            Estado = query.GetBoolean(4),
                                            Cliente = query.GetInt32(2),
                                            Cadete = query.IsDBNull(3) ? null : query.GetInt32(3)

                });
                   
            }
            conexion.Close();

            
        }

        public Pedidos PedidoPorNro(int nro){

            return this.ListaPedidos.FirstOrDefault(e => e.Nro == nro);

        }

        public List<Pedidos> TodosPedidos(){
            return this.ListaPedidos;
        }
        public bool SubirPedido(Pedidos Pedido){
            conexion.Open();
            SqliteCommand insertar = new("INSERT INTO pedidos (observacion,id_cliente,id_cadete,estado) VALUES (@obs, @idcli, @idca, @est)", conexion);
            insertar.Parameters.AddWithValue("@obs", Pedido.Obs);
            insertar.Parameters.AddWithValue("@idcli", Pedido.Cliente);
            insertar.Parameters.AddWithValue("@idca", Pedido.Cadete);
            insertar.Parameters.AddWithValue("est",Pedido.Estado);
             try
            {
                insertar.ExecuteReader();
                conexion.Close();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                conexion.Close();
                return false;
            }

        }
        public bool EliminarPedido(string Nro){
            conexion.Open();
            SqliteCommand select = new SqliteCommand("DELETE FROM pedidos WHERE id_pedido = @id", conexion);
            select.Parameters.AddWithValue("@id",Int32.Parse(Nro));
             try
            {
                select.ExecuteReader();
                conexion.Close();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                conexion.Close();
                return false;
            }
           


        } 


    }
}