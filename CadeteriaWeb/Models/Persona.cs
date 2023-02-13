using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models
{
    public class Persona
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public Persona(int iD, string nombre, string direccion, string telefono)
        {
            ID = iD;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }

        public override string ToString()
        {
            return $"ID: {ID}\nNombre: {Nombre}\nDireccion: {Direccion}\nTelefono: {Telefono}";
        }
    }
}