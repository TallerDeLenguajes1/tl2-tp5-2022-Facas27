using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data;
using Microsoft.Data.Sqlite;


namespace CadeteriaWeb.Models.CadetesModels
{
    public class CadetesRepositorio:ICadetes
    {
        
      private List<Cadetes> ListaCadetes {get;set;}

      private readonly IConfiguration Configuration;
      
      

      public CadetesRepositorio(IConfiguration configuration){
        ListaCadetes = new List<Cadetes>();
        Configuration = configuration;

        
        
      }

      public Cadetes CadetePorID(int id)
            {
                Cadetes cadete = null;
                SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
                conexion.Open();
                SqliteCommand comando = new();
                comando.Connection = conexion;
                SqliteDataReader reader;

                try
                {
                    comando.CommandText = "SELECT * FROM cadetes WHERE id_cadete = $id";
                    comando.Parameters.AddWithValue("$id", id);
                    reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        cadete = new Cadetes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ha ocurrido un error (ClienteRepo, Obtener): " + ex.Message);
                }

              conexion.Close();

              return cadete;
            }

        public List<Cadetes> TodosCadetes(){
            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
            conexion.Open();
            try
            {
                SqliteCommand select = new SqliteCommand("SELECT * FROM cadetes", conexion);
                var query = select.ExecuteReader();
                while (query.Read())
                    {                                          //ID,          Nombre               Direc         Telefono           IDCadeteria
                        ListaCadetes.Add(new Cadetes(query.GetInt32(0), query.GetString(1), query.GetString(2), query.GetString(3)));
                    }
                }
            catch (System.Exception ex)
            {
                
                Console.WriteLine("Ha ocurrido un error (CadeRepo, TodosCadetes): " + ex.Message);
            }
            
            conexion.Close();
            return ListaCadetes;
        }
        public bool SubirCadetes(Cadetes Cadete){
            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
            conexion.Open();
            SqliteCommand insertar = new("INSERT INTO cadetes (nombre,direccion,telefono) VALUES (@nom, @dire, @tel)", conexion);
            insertar.Parameters.AddWithValue("@nom", Cadete.Nombre);
            insertar.Parameters.AddWithValue("@dire", Cadete.Direccion);
            insertar.Parameters.AddWithValue("@tel", Cadete.Telefono);
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

        public bool EliminarCadetes(string ID){
            SqliteConnection conexion = new SqliteConnection(Configuration["ConnectionStrings:Connection"]);
            conexion.Open();
            SqliteCommand select = new SqliteCommand("DELETE FROM cadetes WHERE id_cadete = @id", conexion);
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

       
           





    }
}