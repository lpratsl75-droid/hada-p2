using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hada;

namespace Hada
{
    internal class Barco
    {
        public Dictionary<Coordenada, String> CoordenadasBarco {  get; private set; }

        public String Nombre { get; set; }

        public int NumDanyos { get; set; }

        public event EventHandler<TocadoArgs> eventoTocado;
        public event EventHandler<HundidoArgs> eventoHundido;

        public Barco(string nombre, int longitud, char
        orientacion, Coordenada coordenadaInicio)
        {

            CoordenadasBarco = new Dictionary<Coordenada, string>();

            if (orientacion == 'h')
            {
                for (int i = 0; i < longitud; i++)
                {
                    CoordenadasBarco.Add(new Coordenada(coordenadaInicio.Fila , coordenadaInicio.Columna + i), nombre);

                }
            }
            if (orientacion == 'v')
            {
                for (int i = 0; i < longitud; i++)
                {
                    CoordenadasBarco.Add(new Coordenada(coordenadaInicio.Fila + i , coordenadaInicio.Columna), nombre);

                }
            }
            Nombre = nombre;
            NumDanyos = 0;
        }

        public void Disparo(Coordenada c)
        {
            if (CoordenadasBarco.ContainsKey(c))
            {
                if (!CoordenadasBarco[c].EndsWith("_T"))
                {
                    CoordenadasBarco[c] += "_T";
                    this.NumDanyos++;
                }

                if(eventoTocado != null)
                {
                    eventoTocado(this, new TocadoArgs(CoordenadasBarco[c], c));
                }

                if (NumDanyos == CoordenadasBarco.Count() && eventoHundido != null)
                {
                    eventoHundido(this, new HundidoArgs(CoordenadasBarco[c]));
                }
            }
        }

        public bool hundido()
        {
            bool derrivado = true;

            foreach (String nom in CoordenadasBarco.Values)
            {
                if(nom == Nombre)
                {
                    derrivado = false;
                }
            }
            return derrivado;
        }
        public override string ToString()
        {
            string resultado = "[" + Nombre + "] - DAÑOS: [" + NumDanyos + "] - HUNDIDO: [" + hundido() + "] - COORDENADAS: ";

            foreach (var par in CoordenadasBarco)
            {
                resultado = resultado + "[" + par.Key.ToString() + " :" + par.Value + "]";
            }

            return resultado.;
        }
    }
}
