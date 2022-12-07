using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Zmey
{
    public struct Pixel
    {
        private const char PixelChar = '∎';
        private readonly int pixSize;

        public Pixel(int x, int y, ConsoleColor color, int pixSize = 3) 
        {
            X = x;
            Y = y;
            PixSize = pixSize;
            Color = color;
        }

        public int X { get; }
        public int Y { get; }   
        public ConsoleColor Color { get; }
        public int PixSize { get; }

        public void Draw()
        {
            Console.ForegroundColor = Color;
            for (int x = 0; x < PixSize; x++)
            {
                for (int y = 0; y < PixSize; y++)
                {
                    Console.SetCursorPosition( left: X * PixSize + x, top:Y * PixSize);
                    Console.WriteLine(PixelChar);
                }
            }
        } 
        public void Clear()
        {
            for (int x = 0; x < PixSize; x++)
            {
                for (int y = 0; y < PixSize; y++)
                {
                    Console.SetCursorPosition(left: X * PixSize + x, top: Y * PixSize);
                    Console.WriteLine(' ');
                }
            }
        }
    }
}
