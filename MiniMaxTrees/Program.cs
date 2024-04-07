using System;

namespace MiniMaxTrees
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameController game = new GameController();

            while(!game.GameOver)
            {
                game.Print();
                int current;
                int target;

                Console.WriteLine("Select a piece: ");
                current = int.Parse(Console.ReadLine());

                Console.WriteLine("Select a spot: ");
                target = int.Parse(Console.ReadLine());

                game.Move(current, target);
            }

        }
    }
}
