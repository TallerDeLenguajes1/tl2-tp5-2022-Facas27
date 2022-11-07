using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data;
using Microsoft.Data.Sqlite;

namespace CadeteriaWeb.Models.CadeteriaModels
{
    public class CadeteriaRepositorio:ICadeteria
    {
        private List<Cadeteria> ListaCadeterias {get;set;}
        static public SqliteConnection conexion = new SqliteConnection($"Filename=CadeteriaWeb.db");

        public CadeteriaRepositorio(){
            ListaCadeterias = new List<Cadeteria>();
            conexion.Open();
            SqliteCommand select = new SqliteCommand("SELECT * FROM cadeteria", conexion);
            var query = select.ExecuteReader();
            while (query.Read())
                {                                          //ID,          Nombre             
                    ListaCadeterias.Add(new Cadeteria(query.GetInt32(0), query.GetString(1)));
                }
            conexion.Close();
            }


            public Cadeteria CadeteriaPorID(int id)
            {
                return this.ListaCadeterias.FirstOrDefault(e => e.ID == id);
            }

            public List<Cadeteria> TodosCadeteria(){
             return this.ListaCadeterias;
            }


    }
}