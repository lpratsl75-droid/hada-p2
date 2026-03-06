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
            Barco b1 = new Barco("Barco1", 1, 'a' , new Coordenada(0,0));
            Barco b2 = new Barco("Barco2", 2, 'b' , new Coordenada(1,1));
            Barco b3 = new Barco("Barco3", 3, 'c' , new Coordenada(2,2));

            barcos.Add(b1);
            barcos.Add(b2);
            barcos.Add(b3);


            Tablero tablero = new Tablero(8, barcos);
            tablero.eventoFinPartida += cuandoEventoFinPartida;

            string respuesta = "n";

            do{
                Console.WriteLine(tablero.DibujarTablero());
                Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para Salir): ");
                respuesta = Console.ReadLine();

                int x, y;


                // Comprobar coordenada
                if (respuesta.Length == 3 && int.TryParse(respuesta[0].ToString(), out x) && int.TryParse(respuesta[2].ToString(), out y))
                {
                    Coordenada c = new Coordenada(x, y);
                    tablero.Disparar(c);

                    respuesta = "s";
                }


            }while(respuesta != "s" || respuesta != "S");
        

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
