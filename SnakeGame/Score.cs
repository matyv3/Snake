using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame
{
    public class Score
    {
        private string FilePath { get; set; }

        public Score()
        {
            this.FilePath = "./Scores.txt";
        }

        /// <summary>
        /// Shows the sore.
        /// </summary>
        public void ShowSore()
        {
            Console.Clear();
            Tablero T = new Tablero();
            Console.ForegroundColor = ConsoleColor.White;
            T.DibujarTableroMenu();
            string title = "Puntajes:";
            int startPosition = 8;
            T.Move(startPosition, (T.Width / 2) - (title.Length / 2));
            Console.ForegroundColor = ConsoleColor.Green;
            T.Print(title);
            startPosition++;
            string msg = "Presione Escape para volver al menú principal";
            T.Move(startPosition, (T.Width / 2) - (msg.Length / 2));
            Console.ForegroundColor = ConsoleColor.White;
            T.Print(msg);
            startPosition++;
            string titulos = "Jugador  -  Puntaje  -  Fecha";
            startPosition++;
            T.Move(startPosition, (T.Width / 2) - (titulos.Length / 2));
            Console.ForegroundColor = ConsoleColor.Magenta;
            T.Print(titulos);
            startPosition++;
            Console.ForegroundColor = ConsoleColor.White;
            List<ScoreLine> lines = new List<ScoreLine>();

            // leo y guardo cada linea del documento en una lista
            using (System.IO.StreamReader reader = new System.IO.StreamReader(this.FilePath))
            { 
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    if(!string.IsNullOrEmpty(line))
                    {
                        ScoreLine scoreLine = new ScoreLine();
                        string[] data = line.Split('-');
                        scoreLine.PlayerName = data[0];
                        scoreLine.Score = data[1];
                        scoreLine.Date = data[2];
                        lines.Add(scoreLine);
                    }
                }
            }

            // creo una nueva lista ordenada por puntajes
            // https://stackoverflow.com/questions/3925258/c-sharp-list-orderby-descending
            List<ScoreLine> SortedLines = lines.OrderByDescending(x => x.Score ).ToList();

            // imprimo cada puntaje
            foreach (ScoreLine line in SortedLines)
            {
                startPosition++;
                string str = line.PlayerName + "  -  " + line.Score + "  -  " + line.Date;
                T.Move(startPosition, (T.Width / 2) - (str.Length / 2));
                T.Print(str);
            }
            T.Move(T.Bottom, 0);
        }

        /// <summary>
        /// Guarda el puntaje en un txt
        /// </summary>
        /// <param name="playerName">Player name.</param>
        /// <param name="points">Points.</param>
        public void SaveScore(string playerName, int points)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(this.FilePath, true))
            {
                file.WriteLine(playerName + "-" + points.ToString() + "-" + DateTime.Now.ToString("d/M/yyyy"));
            }
        }

        /// <summary>
        /// Gets the highest score.
        /// </summary>
        /// <returns>The highest score.</returns>
        public int GetHighestScore()
        {
            int highestScore = 0;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(this.FilePath))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] data = line.Split('-');

                        int score = Int32.Parse(data[1]);
                        if(score > highestScore)
                        {
                            highestScore = score;
                        }
                    }
                }
            }
            return highestScore;
        }
    }
}
