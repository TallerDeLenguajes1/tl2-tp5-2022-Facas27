using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CadeteriaWeb.ViewModels
{
    public class CadetesViewModels
    {
        

        public int ID {get;set;}

        [Required][StringLength(100)][Display(Name="Nombre del Cadete")]
        public string Nombre{get;set;}

        [Required][StringLength(100)][Display(Name="Direccion del Cadete")]
        public string Direccion {get;set;}

        [Required][Phone][Display(Name="Telefono del Cadete")]
        public string  Telefono {get;set;}


        public CadetesViewModels(){
            this.ID = 0;
            foreach (string cadete in File.ReadAllLines(@"Cadeteria.csv"))
            {
                if (cadete != "")
                {
                    this.ID = Int32.Parse(cadete.Split(";")[0])+1;
                }
            }

        }

        public CadetesViewModels(int id,string nombre, string dire, string tel){

            Nombre = nombre;
            Direccion = dire;
            Telefono = tel;
            ID = id;

            
        }


    }
}