using System;
namespace SnakeGame
{
    public class Position
    {
        public int x;
        public int y;
        public char c;

        public Position(char symbol = 'O')
        {
            c = symbol;
        }
    }
}
