using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp5;

namespace Zmey
{
    public class Zmey
    {
        private readonly ConsoleColor headCol;
        private readonly ConsoleColor bodyCol;

        public Zmey(int initialX, int initialY, ConsoleColor headCol, ConsoleColor bodyCol, int bodyLength = 3)
        {
            this.headCol = headCol;
            this.bodyCol = bodyCol;

            Head = new Pixel(initialX, initialY, this.headCol);

            for (int i = bodyLength; i >= 0; i--)
            {
                Body.Enqueue(new Pixel(Head.X - i - 1, initialY, this.bodyCol));
            }
            Draw();
        }

        public Pixel Head { get; private set; }

        public Queue<Pixel> Body { get; } = new Queue<Pixel>();

        public void dvizh(Direction direction, bool eat = false)
        {
            Clear();
            Body.Enqueue(new Pixel(Head.X, Head.Y, this.bodyCol));
            if(!eat)
                Body.Dequeue();

            Head = direction switch
            {
                Direction.Left => new Pixel(Head.X - 1, Head.Y, this.headCol),
                Direction.Right => new Pixel(Head.X + 1, Head.Y, this.headCol),
                Direction.Up => new Pixel(Head.X, Head.Y - 1, this.headCol),
                Direction.Down => new Pixel(Head.X, Head.Y + 1, this.headCol),
                _ => Head
            };
            Draw();
        }

        public void Draw()
        {
            Head.Draw();

            foreach (Pixel pixel in Body)
            {
                pixel.Draw();
            }
        }
        public void Clear()
        {
            Head.Clear();

            foreach (Pixel pixel in Body)
            {
                pixel.Clear();
            }
        }
    }
}
