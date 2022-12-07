using System;
using System.Diagnostics;
using ConsoleApp5;
using static System.Console;

namespace Zmey
{
    class Program
    {
        private const int MapWidth = 40;
        private const int MapHeight = 20;

        private const int ScWidth = MapWidth * 3;
        private const int ScHeight = MapHeight * 3;

        private const int fps = 200;

        private const ConsoleColor BordColor = ConsoleColor.Gray;

        private const ConsoleColor headCol = ConsoleColor.DarkBlue;
        private const ConsoleColor bodyCol = ConsoleColor.Red;
        private const ConsoleColor foodCol = ConsoleColor.Green;

        private static readonly Random Random = new Random();

        static void Main()
        {
            SetWindowSize(ScWidth, ScHeight);
            SetBufferSize(ScWidth, ScHeight);
            CursorVisible = false;
            while (true)
            {
                StartGame();

                Thread.Sleep(2500);
                ReadKey();
            }
        }

        static void StartGame()
        {
            Clear();
            DrawBord();
            Direction currentDvizh = Direction.Right;

            var zmey = new Zmey(10, 5, headCol, bodyCol);

            Pixel food = GFood(zmey);
            food.Draw();
            int yoer_ball = 0;
            Stopwatch sw =  new Stopwatch();

            while (true)
            {
                sw.Restart();

                Direction oldDvizh = currentDvizh;

                while (sw.ElapsedMilliseconds < fps)
                {
                    if(currentDvizh == oldDvizh)
                    {
                        currentDvizh = ReadDvizh(currentDvizh);

                    }
                }
                if(zmey.Head.X == food.X && zmey.Head.Y == food.Y)
                {
                    zmey.dvizh(currentDvizh, true);
                    food = GFood(zmey);
                    food.Draw();
                    yoer_ball++;
                }
                else 
                {
                    zmey.dvizh(currentDvizh);
                }


                if (zmey.Head.X == MapWidth - 1 || zmey.Head.X == 0 || zmey.Head.Y == MapHeight - 1 || zmey.Head.Y == 0
                    || zmey.Body.Any(b => b.X == zmey.Head.X && b.Y == zmey.Head.Y))
                    break;
            }
            zmey.Clear();
            SetCursorPosition(ScWidth / 3, ScHeight / 3);
            WriteLine($"Game_Over. Your score: {yoer_ball}");
        }

        static Pixel GFood(Zmey zmey)
        {
            Pixel food;
            do
            {
                food = new Pixel(Random.Next(1, MapWidth - 2), Random.Next(1, MapHeight - 2), foodCol);
            } while (zmey.Head.X == food.X && zmey.Head.Y == food.Y 
                     || zmey.Body.Any(b => b.X == food.X && b.Y == food.Y));
            return food;
        }

        static Direction ReadDvizh(Direction currentDirection)
        {
            if (!KeyAvailable)
                return currentDirection;
            ConsoleKey kay = ReadKey(intercept: true).Key;

            currentDirection = kay switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                _ => currentDirection
            };

            return currentDirection;
        }

        static void DrawBord()
        {
            for (int i = 0; i < MapWidth; i++)
            {
                new Pixel(x: i, y: 0, BordColor).Draw();
                new Pixel(x: i, y: MapHeight - 1, BordColor).Draw();
            }
            for (int i = 0; i < MapHeight; i++)
            {
                new Pixel(x: 0, y: i, BordColor).Draw();
                new Pixel(x: MapWidth - 1, y: i, BordColor).Draw();
            }
        }
    }
}
