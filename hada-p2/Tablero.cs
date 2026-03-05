using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hada_p2
{
    internal class Tablero
    {

        private int tamano;

        public int TamTablero
        {
            get
            {
                return this.tamano;
            }

            set
            {
                if(tamano < 4 || tamano > 9)
                {
                    throw new ArgumentOutOfRangeException("Value está fueta de rango");
                }
            }
        }

       

    }
}
