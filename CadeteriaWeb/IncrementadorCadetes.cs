using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb
{
    public class IncrementadorCadetes
    {
        public  int contador {get;set;}


        public IncrementadorCadetes(){
            contador = -1;
        }
        public int ObtenerContador(){
            this.contador  = contador+1;
            return contador;
        }




    }
}