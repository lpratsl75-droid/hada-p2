using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hada;

namespace Hada
{
    internal class TocadoArgs : EventArgs
    {
        public String nombre { get; set; }
        public Coordenada coordenadaImpacto { get; set;}

        public TocadoArgs(string nombre, Coordenada coordenadaImpacto )
        {
            this.nombre = nombre;
            this.coordenadaImpacto = coordenadaImpacto;
        }
    }

    internal class HundidoArgs : EventArgs
    {   
        public String nombre { get;  set; }

        public HundidoArgs(string nombre)
        {
            this.nombre = nombre;
        }

    }
}
