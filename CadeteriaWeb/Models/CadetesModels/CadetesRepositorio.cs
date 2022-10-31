using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models.CadetesModels
{
    public class CadetesRepositorio:ICadetes
    {
      private List<Cadetes> ListaCadetes {get;set;}
      
      

      public CadetesRepositorio(){
        ListaCadetes = new List<Cadetes>();
        foreach (string cadete in File.ReadAllLines(@"Cadeteria.csv"))
            {
                if (cadete != "")
                {
                     string[]datos = cadete.Split(";");
                                                               //ID,    Nombre    Direc  Telefono
                    ListaCadetes.Add(new Cadetes(Int32.Parse(datos[0]),datos[1],datos[2], datos[3]));
                }
            }
      }

      public Cadetes CadetePorID(int id)
            {
                return this.ListaCadetes.FirstOrDefault(e => e.ID == id);
            }

        public List<Cadetes> TodosCadetes(){
            return this.ListaCadetes;
        }
        public void SubirCadetes(Cadetes Cadete){
            string path = @"Cadeteria.csv";
            ListaCadetes.Add(Cadete);
            File.WriteAllText(path,"");
            foreach (var item in ListaCadetes)
            {
                string cad = $"{item.ID};{item.Nombre};{item.Direccion};{item.Telefono}\n";
                File.AppendAllText(path, cad);
            }

        }
        public bool EliminarCadetes(string ID){
            string path = @"Cadeteria.csv";
            string[]leer = System.IO.File.ReadAllLines(path);
            System.IO.File.WriteAllText(path,"");
            bool band = false;
            for (int i = 0; i < leer.Length; i++)
            {
                string[]datos = leer[i].Split(";");
                if ( ID == datos[0].Trim())
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