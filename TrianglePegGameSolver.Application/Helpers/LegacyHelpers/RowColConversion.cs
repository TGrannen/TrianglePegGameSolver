using System.Collections.Generic;
using LegacyTrianglePegGame;

namespace TrianglePegGameSolver.Application.Helpers
{
    public class RowColConversion
    {
        private readonly Dictionary<(int, int), int> _gridToNumberDictionary = new Dictionary<(int, int), int>();
        private readonly Dictionary<int, (int, int)> _numberToGridDictionary = new Dictionary<int, (int, int)>();

        public RowColConversion()
        {
            var board = new LegacyPegBoard();
            board.InitBoard();
            int count = 1;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    LegacyPegLocation loc = board.boardArray[i, j];
                    if (loc.isValid)
                    {
                        _numberToGridDictionary.Add(count, (i, j));
                        _gridToNumberDictionary.Add((i, j), count);

                        count++;
                    }
                }
            }
        }

        public (int, int) ConvertToGridLocation(int number)
        {
            return _numberToGridDictionary[number];
        }

        public int ConvertToHoleNumber(LegacyPegLocation location)
        {
            return _gridToNumberDictionary[(location.location.row, location.location.col)];
        }
    }
}