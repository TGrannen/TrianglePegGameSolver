using System;
using System.Collections.Generic;

namespace LegacyTrianglePegGame
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Logger log = new Logger();
            PegGame game = new PegGame();
            try
            {
                game.InitGame();
                game.board.PrintTutorialBoard_Row();
                Logger.WriteToScreen("Enter the row: ");
                string temp = Console.ReadLine();
                int row = Convert.ToInt32(temp);

                game.board.PrintTutorialBoard_Col();
                Logger.WriteToScreen("Enter the column: ");
                temp = Console.ReadLine();
                int Col = Convert.ToInt32(temp);

                game.board.EmptyPeg(row, Col);
                game.board.PrintBoard();
                List<HistoricalMove> moves = new List<HistoricalMove>();
                game.EvalBoard(moves);
                game.DisplayAnswer(moves);

                game.board.PrintBoard();
            }
            catch (Exception e)
            {
                Logger.WriteToScreen(e.Message);
            }
            Logger.WriteToScreen("Enter To Quit");
            Console.ReadLine();
        }
    }
}