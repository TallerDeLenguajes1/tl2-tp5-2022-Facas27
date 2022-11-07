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
      static public SqliteConnection conexion = new SqliteConnection($"Filename=CadeteriaWeb.db");
      
      

      public CadetesRepositorio(){
        ListaCadetes = new List<Cadetes>();
        conexion.Open();
        SqliteCommand select = new SqliteCommand("SELECT * FROM cadetes", conexion);
        var query = select.ExecuteReader();
        while (query.Read())
            {                                          //ID,          Nombre               Direc         Telefono           IDCadeteria
                ListaCadetes.Add(new Cadetes(query.GetInt32(0), query.GetString(1), query.GetString(2), query.GetString(3),query.GetInt32(4)));
            }
        conexion.Close();
      }

      public Cadetes CadetePorID(int id)
            {
                return this.ListaCadetes.FirstOrDefault(e => e.ID == id);
            }

        public List<Cadetes> TodosCadetes(){
            return this.ListaCadetes;
        }
        public bool SubirCadetes(Cadetes Cadete){
            conexion.Open();
            SqliteCommand insertar = new("INSERT INTO cadetes (nombre,direccion,telefono,id_cadeteria) VALUES (@nom, @dire, @tel, @id_cad)", conexion);
            insertar.Parameters.AddWithValue("@nom", Cadete.Nombre);
            insertar.Parameters.AddWithValue("@dire", Cadete.Direccion);
            insertar.Parameters.AddWithValue("@tel", Cadete.Telefono);
            insertar.Parameters.AddWithValue("@id_cad", Cadete.IDCadeteria);
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