using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace hada_p2
{
    internal class Coordenada
    {

        private int columna;

        // Propiedad Fila
        public int Fila
        {
            get { return this.Fila; }
            set
            {
                if (value < 0 || value > 9)
                {
                    throw new ArgumentOutOfRangeException("Value está fuera de valor");
                }

            }
        }

        // Propiedad Columna
        public int Columna
        {
            get { return valor; }
            set
            {
                minimo = 0;
                maximo = 9;
            }
        }

        // Constructor por defecto
        public Coordenada()
        {

            Fila = 0;
            Columna = 9;

        }

        // Constructor pasado como parámetro fila y columna (tipo int)
        public Coordenada(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }

        // Constructor pasado como parámetro fila y columna (tipo string)
        public Coordenada(int fila, string columna)
        {
            Fila = fila;
            // string columna
        }

        // Constructor pasado como parámetro otra coordenada
        public Coordenada(Coordenada coordenada)
        {
            this.Fila = coordenada.Fila;
            this.Columna = coordenada.Columna;
        }

        // Método ToString
        public override string ToString()
        {
            return "(" + this.Fila + "," + this.Columna + ")";
        }

        // Métedo GetHashCode
        public override int GetHashCode()
        {
            return this.Fila.GetHashCode() ^ this.Columna.GetHashCode();
        }

        // Método Equals pasado como parámetro un objeto
        public override bool Equals(Object obj)
        {
            if (obj.GetType() != typeof(Coordenada))
                return false;

            Coordenada other = (Coordenada)obj;

            return (other.Fila == this.Fila && other.Columna == this.Columna);

        }

        // Método Equals pasado como parámetro una coordenada
        public bool Equals(Coordenada coordenada)
        {
            return (coordenada.Fila == this.Fila && coordenada.Columna == this.Columna);

        }

    }
}
