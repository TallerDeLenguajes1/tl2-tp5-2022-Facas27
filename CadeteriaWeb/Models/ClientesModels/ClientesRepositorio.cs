using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace CadeteriaWeb.Models.ClientesModels
{
    public class ClientesRepositorio:IClientes
    {
        private List<Clientes> ListaClientes {get;set;}
        static public SqliteConnection conexion = new SqliteConnection($"Filename=CadeteriaWeb.db");

        public ClientesRepositorio(){
            ListaClientes = new List<Clientes>();
            conexion.Open();
            SqliteCommand select = new SqliteCommand("SELECT * FROM clientes", conexion);
            var query = select.ExecuteReader();
            while (query.Read())
                {                                          //ID,          Nombre               Direc         Telefono           IDCadeteria
                    ListaClientes.Add(new Clientes(query.GetInt32(0), query.GetString(1), query.GetString(2), query.GetString(3)));
                }
            conexion.Close();
      }
      public Clientes ClientePorID(int id)
            {
                return this.ListaClientes.FirstOrDefault(e => e.ID == id);
            }

        public List<Clientes> TodosCliente(){
            return this.ListaClientes;
        }





        }

        


        
    }
