using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hada;


namespace Hada
{
    internal class Game
    {

        // Propiedad privada
        private bool finPartida = false;

        // Método privado
        private void gameLoop()
        {
            List<Barco> barcos = new List<Barco>();

            // Crear mínimo tres barcos
            Barco b1 = new Barco("Barco1", 1, 'h' , new Coordenada(0,0));
            Barco b2 = new Barco("Barco2", 1, 'h' , new Coordenada(1,1));
            Barco b3 = new Barco("Barco3", 1, 'h' , new Coordenada(2,2));

            barcos.Add(b1);
            barcos.Add(b2);
            barcos.Add(b3);


            Tablero tablero = new Tablero(8, barcos);
            tablero.eventoFinPartida += cuandoEventoFinPartida;

            string respuesta = "n";
            
            while (respuesta != "S") 
            {
                Console.WriteLine(tablero.ToString());
                Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para Salir): ");
                respuesta = Console.ReadLine();

                int x, y;

                if(respuesta ==  "S")
                {
                    finPartida = true;
                }
                int hund = 0;
                for (int i = 0; i < barcos.Count; i++) 
                {
                    if (barcos[i].hundido())
                    {
                        hund++;
                    }
                    if (barcos.Count == hund)
                    {
                        respuesta = "S";
                        finPartida = true;
                    }
                 }
                // Comprobar coordenada
                if (respuesta.Length == 3 && int.TryParse(respuesta[0].ToString(), out x) && int.TryParse(respuesta[2].ToString(), out y))
                {
                    Coordenada c = new Coordenada(x, y);
                    tablero.Disparar(c);

                }


            }
        

            Console.WriteLine("FIN DEL JUEGO");
            finPartida = true; 
        }

        // Constructor público por defecto
        public Game()
        {
            finPartida = false;
            while(!finPartida)
            {
                gameLoop();

            }
        }

        // Evento que se invoca cuando la partida se ha terminado
        private void cuandoEventoFinPartida(object sender, EventArgs e)
        {
            Console.WriteLine("PARTIDA FINALIZADA!!");
            finPartida = true;
        }

    }
}
