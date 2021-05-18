using System.Collections.Generic;
using System.Linq;

namespace TrianglePegGameSolver.Domain
{
    public class PegBoard
    {
        private readonly PegHole[,] _boardArray;

        public PegBoard()
        {
            _boardArray = new PegHole[5, 5];
            int number = 1;
            for (int i = 0; i < 5; i++)
            {
                foreach (int col in Enumerable.Range(0, 5).Where(col => IsCellValid(col, i)))
                {
                    var temp = new PegHole { Filled = true, Number = number };
                    _boardArray[i, col] = temp;
                    number++;
                }
            }

            Holes = GetPegHolesList().ToList();
        }

        public List<PegHole> Holes { get; }

        public int PegsLeft => Holes.Count(x => x.Filled);

        public PegBoard Clone()
        {
            var temp = new PegBoard();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    temp._boardArray[i, j].Filled = _boardArray[i, j].Filled;
                }
            }
            return temp;
        }

        private static bool IsCellValid(int j, int i)
        {
            switch (j)
            {
                case 0:
                case 1 when i > 0:
                case 2 when i > 1:
                case 3 when i > 2:
                case 4 when i > 3:
                    return true;

                default:
                    return false;
            }
        }

        private IEnumerable<PegHole> GetPegHolesList()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var pegHole = _boardArray[i, j];
                    if (pegHole != null)
                    {
                        yield return pegHole;
                    }
                }
            }
        }
    }
}