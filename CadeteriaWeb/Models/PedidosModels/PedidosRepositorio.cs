using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadeteriaWeb.Models.CadetesModels;
using Microsoft.Data.Sqlite;

namespace CadeteriaWeb.Models.PedidosModels
{


   

    public class PedidosRepositorio:IPedidosRepositorio
    {
        private List<Pedidos>ListaPedidos {get;set;}
        private readonly IConfiguration Configuration;

        public PedidosRepositorio(IConfiguration configuration){
            ListaPedidos = new List<Pedidos>();
            Configuration = configuration;
  
        }

        public Pedidos PedidoPorNro(int nro){

                Pedidos pedido = null;
                SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
                conexion.Open();
                SqliteCommand comando = new();
                comando.Connection = conexion;
                SqliteDataReader reader;

                try
                {
                    comando.CommandText = "SELECT * FROM pedidos WHERE id_pedido = $nro";
                    comando.Parameters.AddWithValue("$nro", nro);
                    reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        pedido = new Pedidos{Nro = reader.GetInt32(0),
                                                    Obs =  reader.GetString(1),
                                                    Estado = reader.GetBoolean(4),
                                                    Cliente = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                                                    //SI LO QUE TRAE ES NULO LE PONE EL VALOR null sino su valor
                                                    Cadete = reader.IsDBNull(3) ? null : reader.GetInt32(3)

                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha ocurrido un error (ClienteRepo, Obtener): " + ex.Message);
                }

              conexion.Close();

              return pedido;
            }

        

        public List<Pedidos> TodosPedidos(){
            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
            conexion.Open();
            try
            {
                SqliteCommand select = new SqliteCommand("SELECT * FROM pedidos", conexion);
                var query = select.ExecuteReader();
                while (query.Read())
                    {                               //ID,          OBS             Estado               IDCLIENTE        IDCADETE         
                        ListaPedidos.Add(new Pedidos{Nro = query.GetInt32(0),
                                                    Obs =  query.GetString(1),
                                                    Estado = query.GetBoolean(4),
                                                    Cliente = query.IsDBNull(2) ? null : query.GetInt32(2),
                                                    //SI LO QUE TRAE ES NULO LE PONE EL VALOR null sino su valor
                                                    Cadete = query.IsDBNull(3) ? null : query.GetInt32(3)

                        });
                        
                    }
                    }
            catch (System.Exception ex)
            {
                
                Console.WriteLine("Ha ocurrido un error (PedidoRepo, TodosPedidos): " + ex.Message);
            }
           
            conexion.Close();
            return this.ListaPedidos;
        }
        public bool SubirPedido(Pedidos Pedido){
            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
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
            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
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
        public bool ActualizarPedidos(Pedidos Pedido){
             int resultado = 0;

            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
            SqliteCommand comando = new();
            conexion.Open();

            try
            {
                comando.CommandText = "UPDATE pedidos SET observacion = $obs, id_cliente = $cli, id_cadete = $cad, estado = $est WHERE id_pedido = $id";
                comando.Connection = conexion;
                comando.Parameters.AddWithValue("$obs", Pedido.Obs);
                comando.Parameters.AddWithValue("$cli", Pedido.Cliente);
                comando.Parameters.AddWithValue("$cad", Pedido.Cadete);
                comando.Parameters.AddWithValue("$id", Pedido.Nro);
                comando.Parameters.AddWithValue("$est", Pedido.Estado);
                resultado = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error (PediRepo, Actualizar): " + ex.Message);
            }

            conexion.Close();

            return resultado > 0;

        } 


    }

}