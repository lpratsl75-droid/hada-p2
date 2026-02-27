using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    internal class Barco
    {
        public Dictionary<Coordenada, String> CoordenadasBarco {  get; private set; }

        public String Nombre { get; set; }

        public int NumDanyos { get; set; }

        public Barco(string nombre, int longitud, char
        orientacion, Coordenada coordenadaInicio)
        {
            Nombre = nombre;
            
        }


    }
}
