using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace CadeteriaWeb.Models.ClientesModels
{
    public class ClientesRepositorio:IClientesRepositorio
    {
        private List<Clientes> ListaClientes {get;set;}
         private readonly IConfiguration Configuration;

        public ClientesRepositorio(IConfiguration configuration){
            ListaClientes = new List<Clientes>();
            Configuration = configuration;
            
      }
      public Clientes ClientePorID(int id)
            {
                Clientes cliente = null;
                SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
                conexion.Open();
                SqliteCommand comando = new();
                comando.Connection = conexion;
                SqliteDataReader reader;

                try
                {
                    comando.CommandText = "SELECT * FROM clientes WHERE id_cliente = $id";
                    comando.Parameters.AddWithValue("$id", id);
                    reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        cliente = new Clientes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha ocurrido un error (ClienteRepo, Obtener): " + ex.Message);
                }

              conexion.Close();

              return cliente;
            }

        public List<Clientes> TodosCliente(){
            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
            conexion.Open();
            try
            {
                SqliteCommand select = new SqliteCommand("SELECT * FROM clientes", conexion);
                var query = select.ExecuteReader();
                while (query.Read())
                {                                          //ID,          Nombre               Direc         Telefono           IDCadeteria
                    ListaClientes.Add(new Clientes(query.GetInt32(0), query.GetString(1), query.GetString(2), query.GetString(3)));
                }
                
            }
            catch (System.Exception ex)
            {
                
                Console.WriteLine("Ha ocurrido un error (ClienteRepo, TodosClientes): " + ex.Message);
            }
            
            conexion.Close();
            return this.ListaClientes;
        }
        public bool SubirClientes(Clientes cliente){
            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
            conexion.Open();
            SqliteCommand insertar = new("INSERT INTO clientes (nombre,direccion,telefono) VALUES (@nom, @dire, @tel)", conexion);
            insertar.Parameters.AddWithValue("@nom", cliente.Nombre);
            insertar.Parameters.AddWithValue("@dire", cliente.Direccion);
            insertar.Parameters.AddWithValue("@tel", cliente.Telefono);
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

        public bool EliminarClientes(string ID){
            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]); 
            conexion.Open();
            SqliteCommand select = new SqliteCommand("DELETE FROM clientes WHERE id_cliente = @id", conexion);
            select.Parameters.AddWithValue("@id",Int32.Parse(ID));
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
        public bool ActualizarClientes(Clientes Cliente){
            int resultado = 0;

            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
            SqliteCommand comando = new();
            conexion.Open();

            try
            {
                comando.CommandText = "UPDATE clientes SET nombre = $nom, direccion = $direc, telefono = $tel WHERE id_cliente = $id";
                comando.Connection = conexion;
                comando.Parameters.AddWithValue("$nom", Cliente.Nombre);
                comando.Parameters.AddWithValue("$direc", Cliente.Direccion);
                comando.Parameters.AddWithValue("$tel", Cliente.Telefono);
                comando.Parameters.AddWithValue("$id", Cliente.ID);
                resultado = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error (CadeteRepo, Actualizar): " + ex.Message);
            }

            conexion.Close();

            return resultado > 0;

        }

    
    }

        


        
    }
