using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models.CadeteriaModels
{
    public class Cadeteria
    {
        public int ID{ get; set; }

        public string Nombre { get; set; }


        public Cadeteria(){}


        public Cadeteria(int id, string nom){
            ID = id;
            Nombre = nom; 

        }
    }


}