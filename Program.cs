using System;
using System.Linq;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] moves = GetInput(args);

            MoveResult aiMove = AIMove("Script", moves);

            Console.WriteLine($"HMAC: {aiMove.GetHMAC()}");
            ShowMenu(moves);

            MoveResult myMove = PlayerMove("Player", moves);

            Console.WriteLine($"Player move: {myMove.Move}  \n" +
                              $"Computer move: {aiMove.Move} \n" +
                              $"Result: {GetResult(myMove, aiMove, moves)} \n" +
                              $"HMAC key: {aiMove.GetHMACKey()}");

            GetResult(myMove, aiMove, moves);

            Console.ReadKey();
        }

        private static string GetResult(MoveResult firstMove, MoveResult secondMove, string[] moves)
        {
            int winingCount = (moves.Length - 1) / 2;

            if (firstMove.MoveIndex == secondMove.MoveIndex)
            {
                return "draw";
            }

            for (uint i = firstMove.MoveIndex + 1, j = 0; j < winingCount; i++, j++)
            {
                if (i > moves.Length - 1)
                {
                    i = 0;
                }

                if (i == secondMove.MoveIndex)
                {
                    return secondMove.Name + " win!";
                }
            }

            return firstMove.Name + " win!";
        }

        private static void ShowMenu(string[] moves)
        {
            Console.WriteLine("Available moves:");

            for (int i = 0; i < moves.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {moves[i]}");
            }

            Console.WriteLine("0 - exit \n" +
                "Enter your move: ");
        }

        private static MoveResult PlayerMove(string name, string[] moves)
        {
            uint choice;
            while (!uint.TryParse(Console.ReadLine(), out choice) || choice > moves.Length)
            {
                ShowMenu(moves);
            }

            if (choice == 0)
            {
                Environment.Exit(0);
            }

            return new MoveResult(name, moves, choice - 1);
        }

        private static MoveResult AIMove(string name, string[] moves)
            => new MoveResult(name, moves, (uint)new Random().Next(0, moves.Length));

        private static string[] GetInput(string[] args)
        {
            if (args.Length < 3 || args.Length % 2 == 0)
            {
                throw new ArgumentException("Invalid number of arguments");
            }

            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    for (int j = i + 1; i < args.Length; j++)
                    {
                        if (args[i] == args[j])
                        {
                            throw new ArgumentException("Duplicates found");
                        }
                    }
                }
            }

            return args;
        }
    }
}
