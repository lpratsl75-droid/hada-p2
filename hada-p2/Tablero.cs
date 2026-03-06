using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hada;


namespace Hada
{
    internal class Tablero
    {

        // Atributo privado que indica el tamaño del tablero
        private int tamTablero;

        // Propiedad publica que indica el tamaño del tablero
        public int TamTablero
        {
            get
            {
                return tamTablero;
            }

            set
            {
                if(tamTablero < 4 || tamTablero > 9)
                {
                    throw new ArgumentOutOfRangeException("Value está fueta de rango");
                }

                tamTablero = value;
            }
        }

        // Propiedad privadas
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;

        // Evento fin de partida
        public event EventHandler<EventArgs> eventoFinPartida;


        // Constructor público
        public Tablero(int tamTablero, List<Barco> barcos)
        {
            this.tamTablero = tamTablero;
            this.barcos = new List<Barco>(barcos);

            // Inicializar las propiedades
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();

            // Inicializar los eventos tocado y hundido
            foreach (Barco b in barcos)
            {
                b.eventoTocado += cuandoEventoTocado;
                b.eventoHundido += cuandoEventoHundido;
            }

            // Inicializar casillas del tablero
            inicializarCasillasTablero();
        }   

        // Método privado que inicializa las casillas del tablero
        private void inicializarCasillasTablero()
        {
            for(int i = 0; i < tamTablero; i++)
            {
                for(int j = 0; j < tamTablero; j++)
                {
                    casillasTablero.Add(new Coordenada(i, j), "AGUA");
                }
            }


            // Colocar barcos
            foreach (Barco barco in barcos)
            {
                foreach (Coordenada c in barco.CoordenadasBarco.Keys)
                {
                    this.casillasTablero.Add(c, barco.Nombre);
                }

            }

        }

        // Método público que implementa la función de disparar
        public void Disparar(Coordenada c)
        {
            if(c.Fila < 0 || c.Fila > 9 || c.Columna < 0 || c.Columna > 9)
            {
                string cadenaError = "La coordenada (" + c.Fila + "," + c.Columna + ") está fuera de las dimensiones del tablero.";
                throw new ArgumentOutOfRangeException(cadenaError);
            }

            coordenadasDisparadas.Add(c);

            for(int i = 0; i < barcos.Count; i++)
            {
                Barco b = barcos[i];
                b.Disparo(c);
            }

        }

        // Método público que sirve para dibujar el tablero
        public string DibujarTablero() // Modificar
        {

            string cadenaTablero = "";

            for(int i = 0; i < tamTablero; i++)
            {
                for(int j = 0; j < tamTablero; j++)
                {
                    cadenaTablero += "[" + new Coordenada(i, j) + "]";
                }
                
                cadenaTablero += "\n";
            }

            return cadenaTablero;
        }

        // Método público para devolver una cadena de texto con información
        public override string ToString()
        {
            String cadenaInfo = "";

            for(int i = 0; i < barcos.Count; i++)
            {
                // corregir la cadenaInfo
                cadenaInfo += "[" + barcos[i].Nombre + "] + DAÑOS: [" + barcos[i].CoordenadasBarco.Count + "]";
            }


            return cadenaInfo;

        }



       // Evento que se invoca cuando el barco está tocado
        private void cuandoEventoTocado(object sender, EventArgs e)
        {
            Barco b = (Barco)sender;

            TocadoArgs args = (TocadoArgs)e;
            Coordenada c = args.coordenadaImpacto;

            if (!coordenadasTocadas.Contains(c))
                coordenadasTocadas.Add(c);

            casillasTablero[c] = b.Nombre + "_T";

            Console.WriteLine($"TABLERO: Barco [{b.Nombre}] tocado en Coordenada: [({c.Fila},{c.Columna})]");
        }

        // Evento que se invoca cuando el barco está hundido
        private void cuandoEventoHundido(object sender, EventArgs e)
        {
            Barco b = (Barco)sender;

            if (!barcosEliminados.Contains(b))
                barcosEliminados.Add(b);

            Console.WriteLine($"TABLERO: Barco [{b.Nombre}] hundido!!");

            if (barcosEliminados.Count == barcos.Count)
            {
                eventoFinPartida?.Invoke(this, EventArgs.Empty);
            }
        }



    }
}
