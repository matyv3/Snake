using System;

namespace SnakeGame
{  
    class Program
    {
        public static bool GameOver { get; set; }
        public static int Points { get; set; }
        static Tablero T = new Tablero();
        public enum Status { StartMenu, Game, GameOver, Pause};
        public static Status currentStatus;
        public static Menu M = new Menu();
        public static int GameSpeed { get; set; }
        public static string PlayerName { get; set; }
        public static Score score = new Score();
        public static int HighestScore { get; set; }

        static void Main()
        {

            GameOver = false;

            GameSpeed = 100;

            Points = 0;

            HighestScore = score.GetHighestScore();

            T.DibujarMenu();

            currentStatus = Status.StartMenu;

            while(currentStatus == Status.StartMenu)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            M.SelectPrev();
                            T.DibujarMenu();
                        break;
                        case ConsoleKey.DownArrow:
                            M.SelectNext();
                            T.DibujarMenu();
                        break;
                        case ConsoleKey.Enter:
                            MenuOption option = M.options.Find(f => f.IsChecked == true);
                            Dispatch(option);
                        break;
                    }
                }
            }

        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public static void StartGame()
        {
            Snake S = new Snake();
            S.Move();
            S.GenerateFood();
            while (GameOver == false)
            {
                S.Move();
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow: S.Turn(Snake.Direction.Up); break;
                        case ConsoleKey.DownArrow: S.Turn(Snake.Direction.Down); break;
                        case ConsoleKey.LeftArrow: S.Turn(Snake.Direction.Left); break;
                        case ConsoleKey.RightArrow: S.Turn(Snake.Direction.Right); break;
                    }
                }
                System.Threading.Thread.Sleep(GameSpeed);
            }
            score.SaveScore(PlayerName, Points);
            M.Title = "¡LOSER!";
            score.ShowSore();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        M.Title = "Snake";
                        Main();
                    }
                }
            }
        }

        /// <summary>
        /// Inicia el proceso seleccionado.
        /// </summary>
        /// <param name="option">Option.</param>
        public static void Dispatch(MenuOption option)
        {
            switch (option.MenuAction)
            {
                case MenuOption.Action.Start:

                    AskPlayerName();
                    currentStatus = Status.Game;
                    Console.Clear();
                    T.DibujarTablero();
                    StartGame();
                    break;
                case MenuOption.Action.Scores:
                    score.ShowSore();
                    M.Title = "Snake";
                    while (true)
                    {
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                            if (keyInfo.Key == ConsoleKey.Escape)
                            {
                                Main();
                            }
                        }
                    }
                case MenuOption.Action.Exit:
                    Environment.Exit(0);
                    break;
            }
        }

        /// <summary>
        /// Asks the name of the player.
        /// </summary>
        public static void AskPlayerName()
        {
            Console.Clear();
            string title = "Ingrese su nombre:";
            T.DibujarTableroMenu();
            T.Move(10, (T.Width / 2) - (title.Length / 2));
            T.Print(title);
            T.Move(12, (T.Width / 2) - (title.Length / 2));
            PlayerName = Console.ReadLine();
        }

    }
}
