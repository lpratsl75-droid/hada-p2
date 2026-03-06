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
            // 1. Primero vaciamos el diccionario para evitar basura de ejecuciones previas
            casillasTablero.Clear();

            // 2. Rellenamos el área del tablero con AGUA
            for (int i = 0; i < tamTablero; i++)
            {
                for (int j = 0; j < tamTablero; j++)
                {
                    casillasTablero[new Coordenada(i, j)] = "AGUA";
                }
            }

            // 3. Colocamos los nombres de los barcos encima del AGUA
            foreach (Barco barco in barcos)
            {
                foreach (Coordenada c in barco.CoordenadasBarco.Keys)
                {
                    // IMPORTANTE: Solo colocamos el barco si está dentro de los límites del tablero
                    if (c.Fila < tamTablero && c.Columna < tamTablero)
                    {
                        // USAMOS CORCHETES [] para sobreescribir "AGUA" con el nombre del barco
                        this.casillasTablero[c] = barco.Nombre;
                    }
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
        public string DibujarTablero()
        {
            string cadenaTablero = "";

            for (int i = 0; i < tamTablero; i++)
            {
                for (int j = 0; j < tamTablero; j++)
                {
                    // Creamos la coordenada de la casilla actual
                    Coordenada c = new Coordenada(i, j);

                    // Buscamos el valor en el diccionario (AGUA, NombreBarco o NombreBarco_T)
                    if (casillasTablero.ContainsKey(c))
                    {
                        cadenaTablero += "[" + casillasTablero[c] + "]";
                    }
                }

                // Salto de línea al terminar cada fila
                cadenaTablero += "\n";
            }

            return cadenaTablero;
        }

        // Método público para devolver una cadena de texto con información
        public override string ToString()
        {
            string cadenaInfo = "";

            // 1. Información detallada de cada uno de los Barcos
            // Recorre la lista de barcos y utiliza el ToString de la clase Barco
            foreach (Barco b in this.barcos)
            {
                cadenaInfo += b.ToString() + "\n";
            }

            cadenaInfo += "\n";

            // 2. La lista de ‘Coordenadas Disparadas’
            // Se muestran todas, incluso si están repetidas
            cadenaInfo += "Coordenadas disparadas: ";
            if (coordenadasDisparadas.Count > 0)
            {
                cadenaInfo += string.Join(" ", coordenadasDisparadas);
            }

            cadenaInfo += "\n";

            // 3. La lista de ‘Coordenadas Tocadas’
            // Solo aparecen las coordenadas únicas donde se ha impactado un barco
            cadenaInfo += "Coordenadas tocadas: ";
            if (coordenadasTocadas.Count > 0)
            {
                cadenaInfo += string.Join(" ", coordenadasTocadas);
            }

            cadenaInfo += "\n\n\n";

            // 4. Representación visual del Tablero
            cadenaInfo += "CASILLAS TABLERO\n";
            cadenaInfo += "---------------\n";
            cadenaInfo += DibujarTablero();

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
