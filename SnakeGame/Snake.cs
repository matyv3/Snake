using System;
using System.Collections.Generic;

namespace SnakeGame
{
    public class Snake
    {
        public List<Position> body = new List<Position>();
        public enum Direction { Up, Down, Left, Right };
        public Direction PreviousDirection { get; set; }
        public Tablero T = new Tablero();
        public Position foodPosition = new Position();
        public char FoodChar { get; set; }
        public ConsoleColor snakeColor = ConsoleColor.Green;
        public ConsoleColor foodColor = ConsoleColor.Yellow;

        public Snake()
        {
            this.FoodChar = '@';
            // genero la bicha, arranca con 4 caracteres de cuerpo
            for(int i = 5; i < 9; i++)
            {
                body.Add(new Position { x=i, y=5 });
            }
            // arranca a moverse para la derecha
            this.PreviousDirection = Direction.Right;
        }


        /// <summary>
        /// Mueve la bicha
        /// Añade una posicion nueva en la punta segun el movimiento anterior y quita la cola
        /// </summary>
        public void Move()
        {
            foreach (Position pos in body)
            {
                T.Move(pos.y, pos.x);
                T.Print(pos.c);
            }
            Position tail = body[0];
            Position head = new Position();
            switch (this.PreviousDirection)
            {
                case Direction.Down:
                    head.x = body[body.Count - 1].x;
                    head.y = body[body.Count - 1].y + 1;
                break;
                case Direction.Up:
                    head.x = body[body.Count - 1].x;
                    head.y = body[body.Count - 1].y - 1;
                break;
                case Direction.Left:
                    head.y = body[body.Count - 1].y;
                    head.x = body[body.Count - 1].x - 1;
                break;
                case Direction.Right:
                    head.y = body[body.Count - 1].y;
                    head.x = body[body.Count - 1].x + 1;
                break;
            }
            // me fijo si esta en algun limite o en ella misma
            if(CheckLimit(head)) return;
            // saco la cola
            T.Move(tail.y, tail.x);
            T.Print(' ');
            // me fijo si esta en la posicion del morfi
            if (CheckFood(head))
            {
                Program.Points++; // TODO: ver segun nivel
                Console.ForegroundColor = ConsoleColor.White;
                T.ShowScore();
                GenerateFood();
            }
            else
            {
                // si comio no le saco la cola porque la tiene que alargar
                body.RemoveAt(0);
            }
            body.Add(head);
            T.Move(head.y, head.x);
            Console.ForegroundColor = snakeColor;
            T.Print(head.c);
        }

        /// <summary>
        /// Gira la vivorita
        /// </summary>
        /// <param name="direction">Direction.</param>
        public void Turn(Direction direction)
        {

            switch (direction)
            {
                case Direction.Down:
                    if(this.PreviousDirection != Direction.Up && this.PreviousDirection != Direction.Down)
                    {
                        this.PreviousDirection = direction;
                    }
                    break;
                case Direction.Up:
                    if (this.PreviousDirection != Direction.Down && this.PreviousDirection != Direction.Up)
                    {
                        this.PreviousDirection = direction;
                    }
                    break;
                case Direction.Left:
                    if (this.PreviousDirection != Direction.Left && this.PreviousDirection != Direction.Right)
                    {
                        this.PreviousDirection = direction;
                    }
                    break;
                case Direction.Right:
                    if (this.PreviousDirection != Direction.Right && this.PreviousDirection != Direction.Left)
                    {
                        this.PreviousDirection = direction;
                    }
                    break;
            }

        }

        /// <summary>
        /// Si la bicha se toca a si misma o toca a la pared moris
        /// </summary>
        /// <param name="head">Position.</param>
        public bool CheckLimit(Position head)
        {
           if(head.x == 0 || head.x == T.Middle)
            {
                Program.GameOver = true;
                return true;
            }
           if( head.y == T.Top || head.y == T.Bottom)
            {
                Program.GameOver = true;
                return true;
            }
            int index = body.FindIndex(f => f.x == head.x && f.y == head.y);
            if(index >= 0)
            {
                Program.GameOver = true;
                return true;
            }
            return false;
        }
           
        /// <summary>
        /// Tira comida en algun lado del tablero
        /// </summary>
        public void GenerateFood()
        {
            T.Move(foodPosition.y, foodPosition.x);
            T.Print(' ');
            Console.ForegroundColor = foodColor;
            Random rand = new Random();
            foodPosition.x = rand.Next(2, T.Middle);
            foodPosition.y = rand.Next(T.Top + 1, T.Bottom -1);
            foodPosition.c = this.FoodChar;
            // si es la posicion del cuerpo de la bicha lo genero de nuevo
            int index = body.FindIndex(f => f.x == foodPosition.x && f.y == foodPosition.y);
            while (index >= 0)
            {
                foodPosition.x = rand.Next(1, T.Middle);
                foodPosition.y = rand.Next(T.Top + 1, T.Bottom -1);
                foodPosition.c = this.FoodChar;
            }
            T.Move(foodPosition.y, foodPosition.x);
            T.Print(foodPosition.c);
        }

        /// <summary>
        /// Se fija si la bicha comio o no.
        /// </summary>
        /// <returns><c>true</c>, if food was checked, <c>false</c> otherwise.</returns>
        /// <param name="head">Head.</param>
        public bool CheckFood(Position head)
        {
            return foodPosition.x == head.x && foodPosition.y == head.y;
        }
    }
}
