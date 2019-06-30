using System;

namespace SnakeGame
{
    public class Tablero
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Top { get; private set; }
        public int Bottom { get; private set; }
        public int Middle { get; private set; }

        public Tablero()
        {
            this.Height = 29;
            this.Width = 119;

            this.Bottom = this.Height - 3;
            this.Top = 3;
            this.Middle = this.Width - 30;

            Console.SetWindowSize(this.Width, this.Height);

        }

        /// <summary>
        /// Mueve el cursor
        /// </summary>
        /// <param name="top">Top.</param>
        /// <param name="left">Left.</param>
        public void Move(int top, int left)
        {
            Console.SetCursorPosition(left, top);
        }

        /// <summary>
        /// Imprime en consola
        /// </summary>
        /// <param name="str">String.</param>
        public void Print(string str)
        {
            Console.Write(str);
        }

        public void Print(char str)
        {
            Console.Write(str);
        }

        /// <summary>
        /// Dibuja el marco base del tablero
        /// </summary>
        public void DibujarTablero()
        {
            // primer linea
            for (int i = 0; i < this.Width; i++)
            {
                Move(this.Top, i);
                Print("#");
            }
            // bordes verticales
            for (int i = this.Top; i < this.Bottom; i++)
            {
                // izquierdo
                Move(i, 0);
                Print("#");
                // medio
                Move(i, this.Middle);
                Print("#");
                // derecho
                Move(i, this.Width);
                Print("#");
            }
            // ultima linea
            for (int i = 0; i < this.Width; i++)
            {
                Move(this.Bottom, i);
                Print("#");
            }
            ShowScore();
        }

        /// <summary>
        /// Muestra el puntaje
        /// </summary>
        public void ShowScore()
        {
            int leftMiddle = this.Middle + ((this.Width - this.Middle) / 2);
            // nombre jugador
            Move(this.Top + 2, leftMiddle - ( Program.PlayerName.Length / 2 ));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Print(Program.PlayerName.ToUpper());
            // puntaje
            string pointsStr = "Puntos: " + Program.Points;
            Move(this.Top + 5, leftMiddle - (pointsStr.Length / 2));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Print(pointsStr);
            // muestro el puntaje mas alto
            string highestScoreStr = "Puntaje más alto: ";
            Move(this.Top + 10, leftMiddle - (highestScoreStr.Length / 2));
            Print(highestScoreStr);
            Move(this.Top + 12, leftMiddle - (Program.HighestScore.ToString().Length / 2));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Print(Program.HighestScore.ToString());
            // copywrite ;)
            string copywrite = "Olivera Technologies";
            Move(this.Bottom - 2, leftMiddle - (copywrite.Length / 2));
            Console.ForegroundColor = ConsoleColor.Blue;
            Print(copywrite);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Dibujara el menu.
        /// </summary>
        public void DibujarMenu()
        {
            Console.Clear();
            DibujarTableroMenu();
            // Menu
            Console.ForegroundColor = ConsoleColor.White;

            int optionPos = 12;

            foreach (MenuOption option in Program.M.options)
            {            
                Move(optionPos, (this.Width / 2) - (option.Label.Length / 2));
                Print(option.Label);

                if (option.IsChecked)
                {
                    Move(optionPos, (this.Width / 2) - (option.Label.Length / 2) - 2);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Print(">");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                optionPos += 2;
            }
            Move(this.Bottom + 1, 0);
        }

        /// <summary>
        /// Dibuja el borde sin el menu
        /// </summary>
        public void DibujarTableroMenu()
        {
            // primer linea
            for (int i = 0; i < this.Width; i++)
            {
                Move(this.Top, i);
                Print("#");
            }
            // bordes verticales
            for (int i = this.Top; i < this.Bottom; i++)
            {
                // izquierdo
                Move(i, 0);
                Print("#");
                // derecho
                Move(i, this.Width);
                Print("#");
            }
            // ultima linea
            for (int i = 0; i < this.Width; i++)
            {
                Move(this.Bottom, i);
                Print("#");
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Move(8, (this.Width / 2) - (Program.M.Title.Length / 2));
            Print(Program.M.Title);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
